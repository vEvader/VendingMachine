using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Responces;
using Common.VmEntities;

namespace VendingMachine.Helpers
{
    public static class ExceptionHelper
    {
        public static VmResponse Execute(Func<VmDto> action)
        {
            try
            {
                VmDto result = action();
                return new VmResponse
                {
                    Content = result,
                    IsSuccess = true,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception e)
            {
                var result = Activator.CreateInstance<VmDto>();
                return new VmResponse
                {
                    Content = null,
                    IsSuccess = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}

