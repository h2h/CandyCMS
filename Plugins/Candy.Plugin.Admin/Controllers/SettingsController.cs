using System.Linq;
using System.Web.Mvc;

using Candy.Core.Domain;
using Candy.Core.Services;

using Candy.Plugin.Admin.Domain;

namespace Candy.Plugin.Admin.Controllers
{
    public class SettingsController : BaseAdminController
    {
        private readonly ISettingService _settingService;
        private readonly ILanguageService _languageService;

        public SettingsController(ISettingService settingService,
            ILanguageService languageService)
        {
            this._settingService = settingService;
            this._languageService = languageService;
        }

        public ActionResult Index()
        {
            var viewModel = new SiteSettingModel();
            viewModel.SiteSettings = this._settingService.LoadSetting<SiteSettings>();
            viewModel.Cultures = this._languageService.GetAllCultureInfo().ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Base(SiteSettings model)
        {
            _settingService.SaveSetting<SiteSettings>(model);

            _settingService.ClearCache();
            return RedirectToAction("Index");
        }
    }
}