using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.FeatureDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeatureController : ControllerBase
	{
		private readonly IFeatureService _featureService;
		private readonly IMapper _mapper;
		public FeatureController(IFeatureService featureService, IMapper mapper)
		{
			_featureService = featureService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult FeatureList()
		{
			var value = _mapper.Map<List<ResultFeatureDto>>(_featureService.TGetListAll());
			return Ok(value);
		}
		[HttpPost]
		public IActionResult CreateFeature(CreateFeatureDto createFeatureDto)
		{
			_featureService.TAdd(new Feature()
			{
				Description = createFeatureDto.Description,
				Title = createFeatureDto.Title
			});
			return Ok("Öne Çıkan Bilgisi Eklendi");
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteFeature(int id)
		{
			var value = _featureService.TGetById(id);
			_featureService.TDelete(value);
			return Ok("Öne Çıkan Bilgisi Silindi");
		}
		[HttpGet("{id}")]
		public IActionResult GetFeature(int id)
		{
			var value = _featureService.TGetById(id);
			return Ok(_mapper.Map<GetFeatureDto>(value));
		}
		[HttpPut]
		public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto)
		{
			_featureService.TUpdate(new Feature()
			{
				FeatureId = updateFeatureDto.FeatureId,
				Description = updateFeatureDto.Description,
				Title = updateFeatureDto.Title
			});
			return Ok("Öne Çıkan Bilgisi Güncellendi");
		}
	}
}
