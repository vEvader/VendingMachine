using System.Web.Mvc;
using Common.Responces;
using Container;
using ProviderInterface;
using VendingMachine.Helpers;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    public class HomeController : Controller
    {
        //1. все вычисления происходят на сервере, чтобы нельзя было "взламать"
        //2. В качетсве зранилища данных использую просто класс в памяти, как самый простой вариант, 
        // но доступ к нем вынесен в одтельный слой и при необходимости, достаточно легко заменить
        //на использование базы или чего-нибудь ещё
        //3. Немного отошёл от задания я считаю введённые деньги в машину как те, которые уже в её кошельке, то есть когда юзер ложить деньги из его кошелька выячитается а в кошельке машины прибавляется.
        //4. Сдача выдаётся сначала купными, потом более мелкими. Т.е. если пользователь введёт две по 5 и попросит сдачу, то ему вернётся одна 10.
        //5. сейчас в гриде с продуктами по две кнопки купить. одна раобтает так как описано в требованиях: всегда включена и если денег недостаточно - показывается алерт, а другая работает так как я бы предложил бы - дизейблится если не остаточно средств.

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult InitModel()
        {
            IVmProvider provider = VmContainer.Instance.Resolve<IVmProvider>();
            VmResponse response = ExceptionHelper.Execute(provider.GetData);
            AjaxResponse ajaxResponse = GetAjaxResponse(response);
            return Json(ajaxResponse);
        }

        [HttpPost]
        public JsonResult EnterCoin(int coinId)
        {
            IVmProvider provider = VmContainer.Instance.Resolve<IVmProvider>();
            VmResponse response = ExceptionHelper.Execute(() =>provider.EnterCoin(coinId));
            AjaxResponse ajaxResponse = GetAjaxResponse(response);
            return Json(ajaxResponse);
        }

        [HttpPost]
        public JsonResult ReturnRest()
        {
            IVmProvider provider = VmContainer.Instance.Resolve<IVmProvider>();
            VmResponse response = ExceptionHelper.Execute(provider.ReturnRest);
            AjaxResponse ajaxResponse = GetAjaxResponse(response);
            return Json(ajaxResponse);
        }

        [HttpPost]
        public JsonResult BuyProduct(int productId)
        {
            IVmProvider provider = VmContainer.Instance.Resolve<IVmProvider>();
            VmResponse response = ExceptionHelper.Execute(() => provider.BuyProduct(productId));
            AjaxResponse ajaxResponse = GetAjaxResponse(response);
            return Json(ajaxResponse);
        }

        #region Private Methods
        private AjaxResponse GetAjaxResponse(VmResponse response)
        {
            AjaxResponse ajaxResponse;
            if (response.IsSuccess)
            {
                VmModel model = MapHelper.Map(response.Content);
                ajaxResponse = AjaxResponse.Successful(string.Empty, model);
            }
            else
            {
                ajaxResponse = AjaxResponse.Successful(response.ErrorMessage, null);
            }
            return ajaxResponse;
        }
        #endregion Private Methods
    }
}
