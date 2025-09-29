using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.TestimonialDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestimonialController : ControllerBase
	{
		private readonly ITestimonialService _testoimonialService;
		private readonly IMapper _mapper;
		public TestimonialController(ITestimonialService testoimonialService, IMapper mapper)
		{
			_testoimonialService = testoimonialService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult TestimonialList()
		{
			var value = _mapper.Map<List<ResultTestimonialDto>>(_testoimonialService.TGetListAll());
			return Ok(value);
		}
		[HttpPost]
		public IActionResult CreateTestimonial(CreateTestimonialDto createTestimonialDto)
		{
			var value = _mapper.Map<Testimonial>(createTestimonialDto);
			_testoimonialService.TAdd(value);
			return Ok("Müşteri Yorum Bilgisi Eklendi");
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteTestimonial(int id)
		{
			var value = _testoimonialService.TGetById(id);
			_testoimonialService.TDelete(value);
			return Ok("Müşteri Yorum Bilgisi Silindi");
		}

		[HttpGet("{id}")]
		public IActionResult GetTestimonial(int id)
		{
			var value = _testoimonialService.TGetById(id);
			return Ok(_mapper.Map<GetTestimonialDto>(value));
		}

		[HttpPut]
		public IActionResult UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
		{
			var value = _mapper.Map<Testimonial>(updateTestimonialDto);
			_testoimonialService.TUpdate(value);
			return Ok("Müşteri Yorum Bilgisi Güncellendi");
		}
	}
}
