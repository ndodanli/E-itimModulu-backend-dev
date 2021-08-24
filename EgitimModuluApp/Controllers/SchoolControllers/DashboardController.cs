using EgitimModuluApp.DataAccessLayer;
using Entities;
using Entities.Dtos;
using Entities.Dtos.SchoolDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EgitimModuluApp.Controllers.SchoolControllers
{
    [Route("api/school/[controller]")]
    [ApiController]
    [Authorize(Role.Admin, Role.School)]
    public class DashboardController : BaseController
    {
        private readonly IDataAccessContext _dataAccess;
        public DashboardController(IDataAccessContext dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        public async Task<ActionResult<List<DashboardDto>>> GetTotalCountByDashboard()
        {
            var dashboardDto = await _dataAccess.SchoolAccess().getTotalCountByDashboard(Account.Id);
            return Ok(dashboardDto);
        }
    }
}
