﻿using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceVendedor
    {
        IEnumerable<Vendedor> GetVendedor();
        IEnumerable<TelefonoVendedor> GetTelefonoByVendedor(int vendedor);

        Vendedor GetVendedorByID(int id);
        void DeleteVendedor(int id);
        Vendedor Save(Vendedor vendedor);

    }
}
