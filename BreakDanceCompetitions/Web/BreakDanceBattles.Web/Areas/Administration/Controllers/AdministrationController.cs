namespace BreakDanceBattles.Web.Areas.Administration.Controllers
{
    using BreakDanceBattles.Common;
    using BreakDanceBattles.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
