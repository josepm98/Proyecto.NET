﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using PlayYourCV.Models;

namespace PlayYourCV.Controllers
{
    public class IdiomasController : BBDDController<Contenido>
    {
        public string _idCat;

        public IdiomasController()
        {
            _table = "contenidos";
            _idCol = "idContenido";
            _idCat = "2";
        }

        public ActionResult Index()
        {
            /*if (checkLogged()!=null)
            {
                ViewBag.UserIsLogged = true;
                ViewBag.Logged = Session["logged"] as String;
                ViewBag.LoggedId = Session["loggedid"] as String;

                ViewData["listaIdiomas"] = GetUserLanguages(ViewBag.LoggedId);
                ViewData["Titulo"] = "Idiomas";
                return View("Index","Login");//return to home
            }
            else
            {
                return checkLogged();
            }*/

            //TODO delete after testing
            ViewData["Titulo"] = "Idiomas";
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Create(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            // TODO: Add update logic here
            return RedirectToAction("Index");
        }

        //BBDD methods
        public List<Contenido> GetUserLanguages(int idUser)
        {
            List<Contenido> list = new List<Contenido>();
            try
            {
                openConn();
                string sql =string.Format("SELECT * FROM {0} WHERE {1}=@uid AND {2}={3}", _table, _idCol, "Categorias_idCategorias", _idCat);
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = _conn;
                cmd.Parameters.AddWithValue("@uid", idUser);
                cmd.Prepare();
                MySqlDataReader rdr = cmd.ExecuteReader();
                list = ToListModel(rdr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
            }
            finally
            {
                closeConn();
            }
            return list;
        }

        public override Contenido ToModel(MySqlDataReader rdr)
        {
            Contenido c = new Contenido();
            if (rdr.Read())
            {
                c = singleContenidoReader(rdr);
            }
            return c;
        }

        public override List<Contenido> ToListModel(MySqlDataReader rdr)
        {
            List<Contenido> list = new List<Contenido>();
            while (rdr.Read())
            {
                list.Add(singleContenidoReader(rdr));
            }
            return null;
        }
		
		private Contenido singleContenidoReader(MySqlDataReader rdr)
        {
            Contenido c = new Contenido();
            c.Id = Convert.ToInt32(rdr[_idCol].ToString());
            c.IdUsuario = Convert.ToInt32(Session["loggedid"] as String);
            c.Hablado = rdr["Hablado"].ToString();
            c.Leido = rdr["Leido"].ToString();
            c.Escrito = rdr["Escrito"].ToString();
            c.NivelGeneral = rdr["NivelGeneral"].ToString();
            return c;
        }
    }
}
