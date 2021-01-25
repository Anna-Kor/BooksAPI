﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Options;

namespace BooksAPI.Models
{
    public class BooksContext
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionStrings _connectionStrings;

        public BooksContext (IConfiguration configuration, IOptions<ConnectionStrings> connectionStrings)
        {
            _configuration = configuration;
            _connectionStrings = connectionStrings.Value;
        }

        public async Task<List<Book>> GetBook()
        {
            List<Book> books = new List<Book>();
            var cs = _configuration.GetValue<string>("ConnectionString:BooksDatabase");
            string query = "SELECT [ID] FROM [dbo].[Books]";

            using (var sqlCon = new SqlConnection(cs))
            {
                using (var cmd = sqlCon.CreateCommand())
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;

                    sqlCon.Open();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            books.Add(bookBuilder(reader));
                        }
                    }
                }
            }

            return books;
        }

        public async Task<List<Book>> GetBook(int id)
        {
            List<Book> books = new List<Book>();
            var cs = _configuration.GetValue<string>("ConnectionString:BooksDatabase");
            string query = "SELECT * FROM [dbo].[Books] WHERE [ID] = " + id.ToString();

            using (var sqlCon = new SqlConnection(cs))
            {
                using (var cmd = sqlCon.CreateCommand())
                {
                    cmd.Connection = sqlCon;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;

                    sqlCon.Open();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            books.Add(bookBuilder(reader));
                        }
                    }
                }
            }

            return books;
        }

        private Book bookBuilder(SqlDataReader reader)
        {
            return new Book
            (
                Guid.Parse(reader["Id"].ToString()),
                reader["Name"].ToString(),
                reader["Author"].ToString(),
                reader["ISBN"].ToString(),
                int.Parse(reader["Year"].ToString())
            );
        }
    }
}
