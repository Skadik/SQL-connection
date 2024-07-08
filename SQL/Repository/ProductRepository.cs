using MySql.Data.MySqlClient;
using SQL.Analyzer.dbAnalyzer;
using SQL.Entity;
using SQL.Service.Data_Base;
using System;
using System.Collections.Generic;

namespace SQL.Repository
{
    public class ProductRepository
    {
        //CRUD

        //отримує перше і останє айді (якщо табалицю відсортовано від мін до макс)
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

        //Read one
        public Product getOne(int id)
        {
            if (!MySqlInspector.checkId(id, "shop", "product"))
            {
                throw new Exception("non-existent id");
            }

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

        //Read all
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

            if (name.Trim().Length == 0)
            { throw new Exception("name length is zero"); }

            if (description.Trim().Length == 0) 
            { throw new Exception("description length is zero"); }

            if (cost <= 0)
            { throw new Exception("cost is less than or equal to zero"); }


            if (!MySqlInspector.checkId(id,"shop","product"))
            {
                throw new Exception("non-existent id");
            }

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
            if (!MySqlInspector.checkId(id, "shop", "product"))
            {
                throw new Exception("non-existent id");
            }

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
