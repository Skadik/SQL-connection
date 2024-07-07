using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using SQL.Entity;
using SQL.Service.Data_Base;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SQL.Repository
{
    public class ProductRepository
    {
        public string getRangeID()
        {
            Singleton.GetInstance().getConnection().Open();
            string sqlRequest =
               "SELECT MIN(`id`),MAX(`id`)" +
               "FROM `product`";
            MySqlCommand comand = new MySqlCommand(sqlRequest, Singleton.GetInstance().getConnection());
            MySqlDataReader reader = comand.ExecuteReader();
            try
            {
                reader.Read();
                int min = Convert.ToInt32(reader["MIN(`id`)"]);
                int max = Convert.ToInt32(reader["MAX(`id`)"]);
                string result = min + " - " + max;
                return result;
            }
            finally
            {
                reader.Close();
                Singleton.GetInstance().getConnection().Close();
            }
        }

        public Product getOne(int id)
        {
            Singleton.GetInstance().getConnection().Open();

            string sqlRequest =
                "SELECT *" +
                "FROM `product`" +
                "WHERE `id` = " + id;
            MySqlCommand comand = new MySqlCommand(sqlRequest, Singleton.GetInstance().getConnection());
            MySqlDataReader reader = comand.ExecuteReader();
            try
            {
                reader.Read();
                Product product = new Product
                {
                    id = Convert.ToInt32(reader["id"]),
                    name = reader["name"].ToString(),
                    description = reader["description"].ToString(),
                    cost = Convert.ToInt32(reader["cost"])
                };
                return product;
            }
            finally
            {
                reader.Close();
                Singleton.GetInstance().getConnection().Close();
            }
        }

        public List<Product> getAll(int start = 0, int limit = 10)
        {
            List<Product> productsList = new List<Product>();
            Singleton.GetInstance().getConnection().Open();

            string sqlRequest =
                "SELECT *" +
                "FROM `product`" +
                "WHERE 1";
            MySqlCommand command = new MySqlCommand(sqlRequest, Singleton.GetInstance().getConnection());
            MySqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        id = Convert.ToInt32(reader["id"]),
                        name = reader["name"].ToString(),
                        description = reader["description"].ToString(),
                        cost = Convert.ToInt32(reader["cost"])
                    };
                    productsList.Add(product);
                }
                return productsList;
            }
            finally
            {
                reader.Close();
                Singleton.GetInstance().getConnection().Close();
            }
        }

        //CRUD

        //Create
        public void create(string name, string discription, int cost)
        {
            Singleton.GetInstance().getConnection().Open();

            string sqlRequest =
                "INSERT INTO `product`(`name`,`description`,`cost`)" +
                $"VALUES('{name}','{discription}','{cost}')";
            try
            {
                MySqlCommand command = new MySqlCommand(sqlRequest, Singleton.GetInstance().getConnection());
                command.ExecuteNonQuery();
            }
            finally
            {
                Singleton.GetInstance().getConnection().Close();
            }
        }

        //Update
        public void update(int id,string name,string description,int cost)
        {
            Singleton.GetInstance().getConnection().Open();
            string sqlRequest =
            "UPDATE `product` " +
            "SET " +
            $"`name` = '{name}'," +
            $"`description` = '{description}'," +
            $"`cost` = '{cost}' " +
            "WHERE `id` = " + id;
            try
            {
                MySqlCommand command = new MySqlCommand(sqlRequest, Singleton.GetInstance().getConnection());
                command.ExecuteNonQuery();
            }
            finally
            {
                Singleton.GetInstance().getConnection().Close();
            }
        }


        //Delete
        public void delete(int id)
        {
            Singleton.GetInstance().getConnection().Open();
            string sqlRequest =
            "DELETE " +
            "FROM `product`" +
            "WHERE `id`=" + id;
            try
            {
                MySqlCommand command = new MySqlCommand(sqlRequest, Singleton.GetInstance().getConnection());
                command.ExecuteNonQuery();
            }
            finally
            {
                Singleton.GetInstance().getConnection().Close();
            }
        }
    }
}
