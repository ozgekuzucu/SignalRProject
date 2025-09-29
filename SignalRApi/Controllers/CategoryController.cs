using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.CategoryDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService categoryService, IMapper mapper)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult CategoryList()
		{
			var value = _mapper.Map<List<ResultCategoryDto>>(_categoryService.TGetListAll());
			return Ok(value);
		}

		[HttpGet("CategoryCount")]
		public IActionResult CategoryCount()
		{
			return Ok(_categoryService.TCategoryCount());
		}
		[HttpGet("ActiveCategoryCount")]
		public IActionResult ActiveCategoryCount()
		{
			return Ok(_categoryService.TActiveCategoryCount());
		}
		[HttpGet("PassiveCategoryCount")]
		public IActionResult PassiveCategoryCount()
		{
			return Ok(_categoryService.TPassiveCategoryCount());
		}
		[HttpPost]
		public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
		{
			_categoryService.TAdd(new Category()
			{
				Name = createCategoryDto.Name,
				Status = true
			});
			return Ok("Kategori eklendi");
		}

		[HttpDelete]
		public IActionResult DeleteCategory(int id)
		{
			var value = _categoryService.TGetById(id);
			_categoryService.TDelete(value);
			return Ok("Kategori Silindi");
		}

		[HttpPut]
		public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			_categoryService.TUpdate(new Category()
			{
				CategoryId = updateCategoryDto.CategoryId,
				Name = updateCategoryDto.Name,
				Status = updateCategoryDto.Status
			});
			return Ok("Kategori Güncellendi");
		}

		[HttpGet("{id}")]
		public IActionResult GetCategory(int id)
		{
			var value = _categoryService.TGetById(id);
			return Ok(value);
		}
	}
}
