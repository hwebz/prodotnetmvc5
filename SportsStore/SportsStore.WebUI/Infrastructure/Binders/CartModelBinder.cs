﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string SessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the Cart from session
            Cart cart = null;
            cart = (Cart) controllerContext.HttpContext.Session[SessionKey];

            // create the Cart if there wasn't one in the session data
            if (cart != null) return cart;
            cart = new Cart();
            controllerContext.HttpContext.Session[SessionKey] = cart;
            // return the cart
            return cart;
        }
    }
}