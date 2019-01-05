using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class ClientWrapper
    {
        private HttpClient client = new HttpClient();

        public ClientWrapper()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:65040/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        void ShowBook(BookModel book)
        {
            Console.WriteLine($"ID: {book.ID}\t Title: {book.Title} \tAuthor: {book.Author}\tGenre: {book.Genre} \tDescription: {book.Description}\t Quantity: {book.Quantity}");
        }

        public void GetAllBooks()
        {
            HttpResponseMessage response = client.GetAsync("api/books").Result;
            if (response.IsSuccessStatusCode)
            {
                // ReadAsAsync is in System.Net.Http.Formatting library
                List<BookModel> books = response.Content.ReadAsAsync<List<BookModel>>().Result;
                if (books.Count > 0)
                {
                    books.ForEach(b => ShowBook(b));
                }
                else
                {
                    Console.WriteLine("No books are returned by the API");
                }
            }
            else
            {
                // error 
                //await DisplayApiError(response);
                string statusCode = string.Format("{0} ({1})", response.StatusCode.ToString(), (int)response.StatusCode);
                string errorText = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("There was an error when calling the API. Details:");
                Console.WriteLine($"Status Code: {statusCode}, Error Text: {errorText}");
            }
        }

        private void DisplayApiError(HttpResponseMessage response)
        {
            string statusCode = string.Format("{0} ({1})", response.StatusCode.ToString(), (int)response.StatusCode);
            string errorText = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("There was an error when calling the API. Details:");
            Console.WriteLine($"Status Code: {statusCode}, Error Text: {errorText}");
        }

        public void GetBookByID(int bookID)
        {
            HttpResponseMessage response = client.GetAsync($"api/books/{bookID}").Result;
            if (response.IsSuccessStatusCode)
            {
                // ReadAsAsync is in System.Net.Http.Formatting library
                BookModel book = response.Content.ReadAsAsync<BookModel>().Result;
                ShowBook(book);
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }

        public void CreateBook(string title, string author, string genre, string description, int quantity)
        {
            BookModel book = new BookModel();
            book.Title = title;
            book.Author = author;
            book.Genre = genre;
            book.Description = description;
            book.Quantity = quantity;

            HttpResponseMessage response = client.PostAsJsonAsync<BookModel>($"api/books", book).Result;
            if (response.IsSuccessStatusCode)
            {
                BookModel newbook = response.Content.ReadAsAsync<BookModel>().Result;
                Console.WriteLine($"Book is saved successfully. The ID of the new book is {newbook.ID}");
                //ShowBook(book);
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }

        public void DeleteBook(int bookID)
        {
            HttpResponseMessage response = client.DeleteAsync($"api/books/{bookID}").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Book is deleted successfully");
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }

        #region Readers

        void ShowReader(ReaderModel reader)
        {
            Console.WriteLine($"ID: {reader.ID}\t FirstName: {reader.FirstName} \tLastName: {reader.LastName}\tPhoneNumber: {reader.PhoneNumber}");
        }

        public void GetAllReaders()
        {
            HttpResponseMessage response = client.GetAsync("api/readers").Result;
            if (response.IsSuccessStatusCode)
            {
                // ReadAsAsync is in System.Net.Http.Formatting library
                List<ReaderModel> readers = response.Content.ReadAsAsync<List<ReaderModel>>().Result;
                if (readers.Count > 0)
                {
                    readers.ForEach(b => ShowReader(b));
                }
                else
                {
                    Console.WriteLine("No readers are returned by the API");
                }
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }

        public void GetReaderByID(int readerID)
        {
            HttpResponseMessage response = client.GetAsync($"api/readers/{readerID}").Result;
            if (response.IsSuccessStatusCode)
            {
                ReaderModel reader = response.Content.ReadAsAsync<ReaderModel>().Result;
                ShowReader(reader);
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }

        public void Createreader(string firstName, string lastName, string phoneNumber)
        {
            ReaderModel reader = new ReaderModel();
            reader.FirstName = firstName;
            reader.LastName = lastName;
            reader.PhoneNumber = phoneNumber;

            HttpResponseMessage response = client.PostAsJsonAsync<ReaderModel>($"api/readers", reader).Result;
            if (response.IsSuccessStatusCode)
            {
                ReaderModel newReader = response.Content.ReadAsAsync<ReaderModel>().Result;
                Console.WriteLine($"Reader is saved successfully. The ID of the new reader is {newReader.ID}");
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }

        public void DeleteReader(int readerID)
        {
            HttpResponseMessage response = client.DeleteAsync($"api/readers/{readerID}").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Reader is deleted successfully");
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }
        #endregion

        #region Borrowed Books
        public void GetReaderBorrowedBook(int readerID)
        {
            HttpResponseMessage response = client.GetAsync($"api/reader/{readerID}/borrowed-books").Result;
            if (response.IsSuccessStatusCode)
            {
                List<BookModel> books = response.Content.ReadAsAsync<List<BookModel>>().Result;
                if (books.Count > 0)
                {
                    books.ForEach(b => ShowBook(b));
                }
                else
                {
                    Console.WriteLine("No books are borrowed for this user");
                }
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }

        public void BorrowBook(int readerID, int bookID)
        {
            HttpResponseMessage response = client.PostAsync($"api/reader/{readerID}/borrowed-books/{bookID}", null).Result;
            if (response.IsSuccessStatusCode)
            {

                Console.WriteLine("The book is borrowed successfully");
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }

        public void ReturnBook(int readerID, int bookID)
        {
            HttpResponseMessage response = client.DeleteAsync($"api/reader/{readerID}/borrowed-books/{bookID}").Result;
            if (response.IsSuccessStatusCode)
            {

                Console.WriteLine("The book is returned successfully");
            }
            else
            {
                // error 
                DisplayApiError(response);
            }
        }
        #endregion
    }
}
