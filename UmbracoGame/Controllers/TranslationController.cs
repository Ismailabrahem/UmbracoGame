using Umbraco.Cms.Core.Services;
using Microsoft.AspNetCore.Mvc;

public class TranslationController : Controller
{
    private readonly ILocalizedTextService _localizedTextService;

    public TranslationController(ILocalizedTextService localizedTextService)
    {
        _localizedTextService = localizedTextService;
    }

    public string GetTranslation(string key)
    {
        return _localizedTextService.Localize("dictionary", key);
    }
}
