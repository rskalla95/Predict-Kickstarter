using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestSharp;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Web.UI.HtmlControls;

namespace Final_Project
{
    public partial class KickstarterPredictor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            SqlConnection cn = new SqlConnection("Server=Tcp:byuis403rskalla.database.windows.net,1433;Initial Catalog = Kickstarter2018;Persist Security Info=False;User ID=rskalla;Password=Temporary415!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=400;");
            SqlCommand cmd;
            SqlDataReader dr;
            ListItem li;

            cn.Open();

            if (ddlSubCategory.Items.Count == 0)
            {
                cmd = new SqlCommand("SELECT DISTINCT sub_category FROM Projects ORDER BY sub_category", cn);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    li = new ListItem((string)dr["sub_category"]);
                    ddlSubCategory.Items.Add(li);
                }
                dr.Close();
            }

            if (ddlCountry.Items.Count == 0)
            {
                cmd = new SqlCommand("SELECT DISTINCT country FROM Projects ORDER BY country", cn);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    li = new ListItem((string)dr["country"]);
                    ddlCountry.Items.Add(li);
                }
                dr.Close();
            }




            DataBind();
            cn.Close();

            if (ddlMonth.Items.Count == 0) {

                ddlMonth.Items.Add("January");
                ddlMonth.Items.Add("February");
                ddlMonth.Items.Add("March");
                ddlMonth.Items.Add("April");
                ddlMonth.Items.Add("May");
                ddlMonth.Items.Add("June");
                ddlMonth.Items.Add("July");
                ddlMonth.Items.Add("August");
                ddlMonth.Items.Add("September");
                ddlMonth.Items.Add("October");
                ddlMonth.Items.Add("November");
                ddlMonth.Items.Add("December");
            }

            if (ddlDay.Items.Count == 0)
            {
                ddlDay.Items.Add("Sunday");
                ddlDay.Items.Add("Monday");
                ddlDay.Items.Add("Tuesday");
                ddlDay.Items.Add("Wednesday");
                ddlDay.Items.Add("Thursday");
                ddlDay.Items.Add("Friday");
                ddlDay.Items.Add("Saturday");
            }
        }

        protected void predictPercentOfGoal(object sender, EventArgs e)
        {
            //Percent of Goal (Regression)
            var client = new RestClient("https://ussouthcentral.services.azureml.net/workspaces/d5029c400994454894e56b3e72920b99/services/d19501403b2c4ff3b1eb35132d391d92/execute?api-version=2.0&details=true");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "0bac25a8-46ce-4b76-924d-67b479030839");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer FYa2S0ZebtIcOrGoke9PRwxk9doysQo64opEKajnV4iI5VipI/tCvOHSKlluZJh+O9EcsmNzuf93Gte33Mn7Jg==");
            request.AddParameter("undefined", "{\n  \"Inputs\": {\n    \"input1\": {\n      \"ColumnNames\": [\n        \"sub_category\",\n        \"country\",\n        \"usd_goal\",\n        \"duration\",\n        \"launch_day\",\n        \"launch_month\",\n        \"percentOfGoal\"\n      ],\n      \"Values\": [\n        [\n          \"" + ddlSubCategory.SelectedItem.Text + "\",\n          \"" + ddlCountry.SelectedItem.Text + "\",\n          \"" + txtGoal.Text + "\",\n          \"" + txtDuration.Text + "\",\n          \"" + ddlDay.SelectedItem.Text + "\",\n          \"" + ddlMonth.SelectedItem.Text + "\",\n          \"0\"\n        ]\n      ]\n    }\n  },\n  \"GlobalParameters\": {}\n}\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var results = JObject.Parse(response.Content);
            string prediction = results["Results"]["output1"]["value"]["Values"].ToString();
            prediction = prediction.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\"", "");
            prediction = prediction.ToString();

            double percent = Convert.ToDouble(prediction);

            percent *= 100;

            lblPercent.Text = "Percent Funded: " + percent.ToString("F2") + "%";
        }
        protected void predictSuccessState(object sender, EventArgs e)
        {
            //Success State (Classification)
            var client = new RestClient("https://ussouthcentral.services.azureml.net/workspaces/d5029c400994454894e56b3e72920b99/services/e507f87f776149b4aaa5721ac2fd14ea/execute?api-version=2.0&details=true");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "827f6dab-2736-4218-9d06-c841ed8a8c80");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer 1bs8kqX2SV9+IO9aXruv9jhCvnmDFHfkhsTKxLs3R5k6fjS5qA1oPxu2CrTWl2gaf3GQkeVkeZoR6oYRUK2+4g==");
            request.AddParameter("undefined", "{\r\n  \"Inputs\": {\r\n    \"input1\": {\r\n      \"ColumnNames\": [\r\n        \"sub_category\",\r\n        \"state\",\r\n        \"duration\",\r\n        \"launch_day\",\r\n        \"launch_month\"\r\n      ],\r\n      \"Values\": [\r\n        [\r\n          \"" + ddlSubCategory.SelectedItem.Text + "\",\r\n          \"\",\r\n          \"" + txtDuration.Text + "\",\r\n          \"" + ddlDay.SelectedItem.Text + "\",\r\n          \"" + ddlMonth.SelectedItem.Text + "\"\r\n        ]\r\n      ]\r\n    }\r\n  },\r\n  \"GlobalParameters\": {}\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var results = JObject.Parse(response.Content);
            string prediction = results["Results"]["output1"]["value"]["Values"].ToString();
            prediction = prediction.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\"", "");

            lblStatus.Text = "Project Status: " + prediction.ToString();
        }

        protected void predictBackers(object sender, EventArgs e)
        {
            //Backers (Regression)
            var client = new RestClient("https://ussouthcentral.services.azureml.net/workspaces/d5029c400994454894e56b3e72920b99/services/a15dd3dbda40415581054b63d8d86428/execute?api-version=2.0&details=true");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "0c5ca8e3-cfdd-4bec-9c97-af2ee81b059e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer Lx4zR7Ga309vHkpy0/ND8ggnuLQArc2ltkUKTq7TZKT9xUQ6nthO6a/SXUAML5G/DAj34/bU0OpY7vjK7O3OMA==");
            request.AddParameter("undefined", "{\r\n  \"Inputs\": {\r\n    \"input1\": {\r\n      \"ColumnNames\": [\r\n        \"sub_category\",\r\n        \"backers\",\r\n        \"country\",\r\n        \"usd_goal\",\r\n        \"duration\",\r\n        \"launch_month\"\r\n      ],\r\n      \"Values\": [\r\n        [\r\n          \"" + ddlSubCategory.SelectedItem.Text + "\",\r\n          \"0\",\r\n          \"" + ddlCountry.SelectedItem.Text + "\",\r\n          \"" + txtGoal.Text + "\",\r\n          \"" + txtGoal.Text + "\",\r\n          \"" + ddlMonth.SelectedItem.Text + "\"\r\n        ]\r\n      ]\r\n    }\r\n  },\r\n  \"GlobalParameters\": {}\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var results = JObject.Parse(response.Content);
            string prediction = results["Results"]["output1"]["value"]["Values"].ToString();
            prediction = prediction.Replace("[", "").Replace("]", "").Replace(" ", "").Replace("\"", "");

            lblBackers.Text = "Backers" + prediction.ToString();
        }

        protected void makePredictions(object sender, EventArgs e)
        {
            predictBackers(sender, e);
            predictSuccessState(sender, e);
            predictPercentOfGoal(sender, e);
            
        }

    }

}