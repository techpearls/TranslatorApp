using System.Web.Mvc;
using TranslatorWithMSApi.Interfaces;
using TranslatorWithMSApi.Models;

namespace TranslatorWithMSApi.Controllers
{
    /// <summary>
    /// Controller to handle incoming requests and generates views
    /// </summary>
    public class TranslateController : Controller
    {
        /// <summary>
        /// Gets or sets the authentication.
        /// </summary>
        /// <value>
        /// The authentication.
        /// </value>
        public IAuthentication Authentication { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateController"/> class.
        /// </summary>
        public TranslateController()
        {
            this.Authentication = new Authentication();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateController"/> class.
        /// </summary>
        /// <param name="auth">The authentication.</param>
        public TranslateController(IAuthentication auth)
        {
            if (auth == null)
            {
                this.Authentication = new Authentication();
            }
            else
            {
                this.Authentication = auth;
            }
        }

        /// <summary>
        /// Generates the view.
        /// </summary>
        /// <returns>the view</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Does the translate.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns>Translated string in JSON format</returns>
        public ActionResult DoTranslate(string text, string source, string target)
        {
            var token = this.Authentication.GetAccessToken();
            var result = TranslationPerformer.Perform(token, text, source, target);

            if (string.IsNullOrWhiteSpace(result))
            {
                return View("Index");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
