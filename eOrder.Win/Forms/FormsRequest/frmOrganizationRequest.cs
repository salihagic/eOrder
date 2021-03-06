﻿using eOrder.CORE.Requests;
using eOrder.Win.Helpers;
using System;
using System.Windows.Forms;

namespace eOrder.Win.Forms.FormsRequest
{
    public partial class frmOrganizationRequest : Form
    {
        APIService _organizationAPIService = new APIService("Organization");
        ComboBoxHelper comboBoxHelper = new ComboBoxHelper();
        private int? _id;

        public frmOrganizationRequest(int? id = null)
        {
            InitializeComponent();
            _id = id;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var request = ControlsHelper.MapControlsToProps(new OrganizationRequest(), gbxOrganizationData);
            request.User = ControlsHelper.MapControlsToProps(new UserRequest(), gbxUserData);

            var result = await _organizationAPIService.Insert<OrganizationDTO>(request);

            if(result != null && result.Id != 0)
                Hide();
        }

        private async void frmOrganizationRequest_Load(object sender, EventArgs e)
        {
            if(cbxCityId.Items.Count == 0)
            {
                cbxCityId = await comboBoxHelper.GetCities(cbxCityId);
            }
            if (cbxOrganizationTypeId.Items.Count == 0)
            {
                cbxOrganizationTypeId = await comboBoxHelper.GetOrganizationTypes(cbxOrganizationTypeId);
            }
            if (cbxCurrencyId.Items.Count == 0)
            {
                cbxCurrencyId = await comboBoxHelper.GetCurrencies(cbxCurrencyId);
            }

            if (_id.HasValue)
            {
                var organizationDTO = await _organizationAPIService.GetById<OrganizationDTO>(_id);
                ControlsHelper.MapPropsToControls(organizationDTO, gbxOrganizationData);
                ControlsHelper.MapPropsToControls(organizationDTO.User, gbxUserData);
            }
        }
    }
}
