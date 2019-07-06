using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using Product.Shared.Items;
using Product.Shared.Requests;
using Product.Shared.Responses;

namespace ProductManagement
{
    public partial class Index : System.Web.UI.Page
    {
        public string BaseApiUrl = "http://localhost/ProductApi/api";

        private string EventId
        {
            get
            {
                if (ViewState["PROD_EVENT_ID"] == null)
                    return "";

                return ViewState["PROD_EVENT_ID"].ToString();
            }
            set { ViewState["PROD_EVENT_ID"] = value; }
        }
        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EventId = Guid.NewGuid().ToString();
                BindData();
            }
        }

        #endregion

        #region Control Events

        protected async void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Trim().Equals("CHANGE"))
            {
                var ctrl = e.CommandSource as ImageButton;

                if (ctrl != null)
                {
                    var row = ctrl.Parent.NamingContainer as GridViewRow;
                    var lblName = (Label)row.FindControl("lblName");
                    var lblQuantity = (Label)row.FindControl("lblQuantity");
                    var lblSaleAmount = (Label)row.FindControl("lblSaleAmount");

                    hidId.Value = e.CommandArgument.ToString().Trim();
                    txtName.Text = lblName.Text.Trim();
                    txtQuanity.Text = lblQuantity.Text.Trim();
                    txtSaleAmount.Text = lblSaleAmount.Text.Trim();

                    btnSave.Visible = true;
                }
            }
            else if (e.CommandName.Trim().Equals("DEL"))
            {
                await Delete(int.Parse(e.CommandArgument.ToString()));
                BindData();
            }
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            var item = GetItem();
            item.Id = int.Parse(hidId.Value);

            var result = await Save(item);

            BindData();
            ClearControl();
        }

        protected async void btnAddNew_Click(object sender, EventArgs e)
        {
            var item = GetItem();

            var result = await Save(item);

            BindData();
            ClearControl();
        }

        #endregion

        #region Functions

        private async Task<ProductResponse> Save(ProductItem item)
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var request = new ProductRequest
            {
                Id = EventId,
                Timestamp = unixTimestamp.ToString(),
                Product = item
            };

            string json = await GetPost("Product", "SaveProduct", request);

            dynamic obj = JsonConvert.DeserializeObject(json);

            ProductResponse response = obj == null ? null : obj.ToObject<ProductResponse>();

            return response;
        }

        private async Task<ProductResponse> Delete(int id)
        {
            var item = new ProductItem
            {
                Id = id
            };
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var request = new ProductRequest
            {
                Id = EventId,
                Timestamp = unixTimestamp.ToString(),
                Product = item
            };

            string json = await GetPost("Product", "Delete", request);

            dynamic obj = JsonConvert.DeserializeObject(json);

            ProductResponse response = obj == null ? null : obj.ToObject<ProductResponse>();

            return response;
        }

        private ProductItem GetItem()
        {
            var item = new ProductItem();
            
            item.Name = txtName.Text.Trim();

            if (!string.IsNullOrEmpty(txtQuanity.Text.Trim()))
                item.Quantity = int.Parse(txtQuanity.Text.Trim());

            if (!string.IsNullOrEmpty(txtSaleAmount.Text.Trim()))
                item.SaleAmount = decimal.Parse(txtSaleAmount.Text.Trim());

            return item;
        }

        private void ClearControl()
        {
            hidId.Value = "";
            txtName.Text = "";
            txtQuanity.Text = "";
            txtSaleAmount.Text = "";
            btnSave.Visible = false;
        }

        private async void BindData()
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var request = new ProductRequest
            {
                Id = EventId,
                Timestamp = unixTimestamp.ToString()
            };

            string json = await GetContent("Product", "GetProducts", request);

            dynamic obj = JsonConvert.DeserializeObject(json);

            ProductResponse response = obj == null ? null : obj.ToObject<ProductResponse>();

            if (response != null)
            {
                gv.DataSource = response.Products;
                gv.DataBind();
            }
        }

        public async Task<string> GetPost(string serviceName, string methodName, object item)
        {
            try
            {
                var _client = new HttpClient();
                var uri = new Uri(string.Format(GetServiceUrl(serviceName, methodName), string.Empty));
                var json = JsonConvert.SerializeObject(item);
                var param = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(uri, param);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }

                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GetContent(string serviceName, string methodName, object item)
        {
            try
            {
                var _client = new HttpClient();
                var uri = new Uri(string.Format(GetServiceUrl(serviceName, methodName), string.Empty));
                var json = JsonConvert.SerializeObject(item);
                var param = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }

                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetServiceUrl(string serviceName, string methodName = null)
        {
            var url = string.Format("{0}/{1}", BaseApiUrl, serviceName);

            if (!string.IsNullOrWhiteSpace(methodName))
                url = string.Format("{0}/{1}", url, methodName);

            return url;
        }

        #endregion
    }
}