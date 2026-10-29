using LivrariaPajeu.API.Services;
using LivrariaPajeu.Communication.Requests;
using LivrariaPajeu.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaPajeu.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookService _service;

    public BooksController(BookService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult Create([FromBody] RequestBookJson request)
    {
        var response = _service.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseBookJson>), StatusCodes.Status200OK)]
    public IActionResult GetAll(
        [FromQuery] string? title,
        [FromQuery] string? author,
        [FromQuery] string? genre)
    {
        var response = _service.GetAll(title, author, genre);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var response = _service.GetById(id);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBookJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult Update([FromRoute] Guid id, [FromBody] RequestBookJson request)
    {
        var response = _service.Update(id, request);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        _service.Delete(id);
        return NoContent();
    }
}
