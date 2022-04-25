using System;
using System.Threading.Tasks;
using AutoMapper;
using LecturesAttendanceSystem.Api.Models;
using LecturesAttendanceSystem.Services.Dtos;
using LecturesAttendanceSystem.Services.Enums;
using LecturesAttendanceSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LecturesAttendanceSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LessonController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILessonService _lessonService;

        public LessonController(
            IMapper mapper,
            ILessonService lessonService)
        {
            _mapper = mapper;
            _lessonService = lessonService;
        }

        /// <summary>
        /// Shows a schedule for current teacher or student.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Collection of lessons and their starting time</returns>
        /// <response code="200">Schedule is returned successfully</response>
        /// <response code="400">If the data is invalid or user does nor exist</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "teacher, student")]
        [HttpGet("schedule/{userId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSchedule(long userId)
        {
            var result = await _lessonService.GetSchedule(userId, DateTime.Today);
            if (result.IsSuccessful)
            {
                return Ok(result.ResultObject);
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Creates a lesson.
        /// </summary>
        /// <param name="newLessonModel">User creating model</param>
        /// <returns>A newly created Lesson</returns>
        /// <response code="201">Returns the newly created lesson</response>
        /// <response code="400">If the data is invalid or lesson doesn't contain any teacher and/or student</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "administrator, teacher")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLesson(NewLessonModel newLessonModel)
        {
            var newLessonDto = _mapper.Map<NewLessonDTO>(newLessonModel);
            var result = await _lessonService.CreateLesson(newLessonDto);
            if (result.IsSuccessful)
            {
                return Created(nameof(CreateLesson), result.ResultObject);
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Edits a lesson.
        /// </summary>
        /// <param name="editLessonModel">User editing model</param>
        /// <param name="lessonId">Lesson ID</param>
        /// <returns>Empty result</returns>
        /// <response code="200">Lesson is edited successfully</response>
        /// <response code="400">If the data is invalid or lesson does not exist</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "administrator, teacher")]
        [HttpPut("{lessonId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditLesson(long lessonId, EditLessonModel editLessonModel)
        {
            var editLessonDto = _mapper.Map<EditLessonDTO>(editLessonModel);
            var result = await _lessonService.EditLesson(lessonId, editLessonDto);
            if (result.IsSuccessful)
            {
                return Ok();
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a lesson.
        /// </summary>
        /// <param name="lessonId">Lesson ID</param>
        /// <returns>Empty result</returns>
        /// <response code="204">Lesson is deleted successfully</response>
        /// <response code="400">If the data is invalid or lesson does not exist</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "administrator, teacher")]
        [HttpDelete("{lessonId:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLesson(long lessonId)
        {
            var result = await _lessonService.DeleteLesson(lessonId);
            if (result.IsSuccessful)
            {
                return NoContent();
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Puts a mark of student's presence on a lesson.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="lessonId">Lesson ID</param>
        /// <returns>Student's ID and his presence mark</returns>
        /// <response code="200">Presence was marked successfully</response>
        /// <response code="400">If a lesson/user does not exist</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "administrator, teacher")]
        [HttpPatch("mark-presence/{lessonId:long}/{userId:long}")]
        public async Task<IActionResult> MarkStudentPresence(long lessonId, long userId)
        {
            var result = await _lessonService.MarkPresence(lessonId, userId);
            if (result.IsSuccessful)
            {
                return Ok(result.ResultObject);
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Counts all absences of all students for the specified period of time.
        /// </summary>
        /// <param name="duration">The period of time to get the absences by</param>
        /// <returns>List of students and their absences' count</returns>
        /// <response code="200">List of absences has been got successfully</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "administrator, teacher")]
        [HttpGet("count-absences/{duration}")]
        public async Task<IActionResult> CountAbsences(AbsencePeriods duration)
        {
            var result = await _lessonService.CountAbsences(duration);
            if (result.IsSuccessful)
            {
                return Ok(result.ResultObject);
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Counts absences of the single student for the specified period of time.
        /// </summary>
        /// <param name="duration">The period of time to get the absences by</param>
        /// <param name="userId">User ID to get its absences</param>
        /// <returns>Student's absences' count</returns>
        /// <response code="200">Student's absences' count has been got successfully</response>
        /// <response code="400">If user does not exist</response>
        /// <response code="500">Any exception thrown</response>
        [Authorize(Roles = "administrator, teacher")]
        [HttpGet("count-absences/{duration}/{userId:long}")]
        public async Task<IActionResult> CountAbsences(AbsencePeriods duration, long userId)
        {
            var result = await _lessonService.CountAbsences(duration, userId);
            if (result.IsSuccessful)
            {
                return Ok(result.ResultObject);
            }
            foreach (var (key, value) in result.Errors)
            {
                ModelState.AddModelError(key, value);
            }
            return BadRequest(ModelState);
        }
    }
}