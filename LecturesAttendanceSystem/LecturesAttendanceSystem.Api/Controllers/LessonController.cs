using System.Threading.Tasks;
using AutoMapper;
using LecturesAttendanceSystem.Api.Models;
using LecturesAttendanceSystem.Services.Dtos;
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

        [Authorize(Roles = "administrator, teacher")]
        [HttpPut("{lessonId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditLesson(long lessonId, EditLessonModel editLessonModel)
        {
            var editLessonDto = _mapper.Map<EditLessonDTO>(editLessonModel);
            ServiceResult result = await _lessonService.EditLesson(lessonId, editLessonDto);
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
    }
}