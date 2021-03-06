﻿using FAD3.Database.Classes;
using SimMetricsMetricUtilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FAD3.Database.Forms
{
    public partial class NewNameForm : Form
    {
        private string _newName;
        private FisheryObjectNameType _objectNameType;
        private Dictionary<string, string> _similarNames = new Dictionary<string, string>();
        private NewFisheryObjectName _newObjectName;
        private string _gearClassGuid;

        public static DialogResult Show(string newName, FisheryObjectNameType objectNameType)
        {
            NewNameForm f = new NewNameForm(newName, objectNameType);
            return f.ShowDialog();
        }

        public static DialogResult Show(NewFisheryObjectName newObjectName, string gearClassGuid = "")
        {
            NewNameForm f = new NewNameForm(newObjectName, gearClassGuid);
            return f.ShowDialog();
        }

        public bool Cancel
        {
            get; internal set;
        }

        public NewNameForm(NewFisheryObjectName newObjectName, string gearClassGuid = "")
        {
            InitializeComponent();
            _newObjectName = newObjectName;
            switch (_newObjectName.NameType)
            {
                case FisheryObjectNameType.CatchLocalName:
                    Text = "New catch local name";
                    _similarNames = Names.GetSimilarSoundingLocalNames(_newObjectName);
                    lblNewType.Text = "New catch local name";
                    lblTitle.Text = " Add new catch local name";
                    break;

                case FisheryObjectNameType.GearLocalName:
                    Text = "New gear local name";
                    _similarNames = Gears.GetSimilarSoundingLocalNames(_newObjectName);
                    lblNewType.Text = "New gear local name";
                    lblTitle.Text = "Add new fishing gear local name";
                    break;

                case FisheryObjectNameType.GearVariationName:
                    Text = "New gear variation name";
                    lblNewType.Text = "New gear variation name";
                    lblTitle.Text = "Add new gear variation name";
                    _gearClassGuid = gearClassGuid;
                    break;

                case FisheryObjectNameType.FishingAccessory:
                    Text = "New fishing accessory";
                    lblNewType.Text = "New accessory name";
                    lblTitle.Text = "Add new fishing accessory";
                    break;

                case FisheryObjectNameType.FishingExpense:
                    Text = "New fishing expense";
                    lblNewType.Text = "New expense category";
                    lblTitle.Text = "Add new fishing expense";
                    break;
            }
            //DoLevenstein();
            txtLocalName.Text = _newObjectName.NewName;
            listBoxSimilar.ValueMember = "key";
            listBoxSimilar.DisplayMember = "value";
            foreach (var item in _similarNames)
            {
                listBoxSimilar.Items.Add(item);
            }
        }

        public NewNameForm(string newName, FisheryObjectNameType objectNameType)
        {
            InitializeComponent();
            _newName = newName;
            _objectNameType = objectNameType;
            _newObjectName = new NewFisheryObjectName(_newName, _objectNameType);
            switch (_objectNameType)
            {
                case FisheryObjectNameType.CatchLocalName:
                    Text = "New catch local name";
                    _similarNames = Names.GetSimilarSoundingLocalNames(_newObjectName);
                    break;

                case FisheryObjectNameType.GearLocalName:
                    Text = "New gear local name";
                    _similarNames = Gears.GetSimilarSoundingLocalNames(_newObjectName);
                    break;
            }
            txtLocalName.Text = newName;
            listBoxSimilar.ValueMember = "key";
            listBoxSimilar.DisplayMember = "value";
            foreach (var item in _similarNames)
            {
                listBoxSimilar.Items.Add(item);
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "btnOk":
                    (bool success, string guid) result = (false, "");
                    switch (_newObjectName.NameType)
                    {
                        case FisheryObjectNameType.CatchLocalName:
                            result = Names.SaveNewLocalName(_newObjectName);
                            break;

                        case FisheryObjectNameType.GearLocalName:
                            result = Gears.SaveNewLocalName(_newObjectName);
                            break;

                        case FisheryObjectNameType.GearVariationName:
                            result = Gears.SaveNewVariationName(_newObjectName, _gearClassGuid);
                            break;

                        case FisheryObjectNameType.FishingAccessory:
                            result.success = Gears.AddAccessory(_newObjectName.NewName);
                            break;

                        case FisheryObjectNameType.FishingExpense:
                            result.success = Gears.AddExpense(_newObjectName.NewName);
                            break;
                    }

                    if (result.success) DialogResult = DialogResult.OK;
                    break;

                case "btnCancel":
                    DialogResult = DialogResult.Cancel;
                    break;
            }
            Close();
        }

        private void DoLevenstein()
        {
            Levenstein lev = new Levenstein();
            switch (_newObjectName.NameType)
            {
                case FisheryObjectNameType.CatchLocalName:
                    var localNameList = Names.LocalNameList;
                    foreach (var item in localNameList)
                    {
                        var similarity = lev.GetSimilarity(_newObjectName.NewName, item);
                    }
                    break;

                case FisheryObjectNameType.GearLocalName:
                    break;
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            listBoxSimilar.Visible = false;
            if (_similarNames.Count > 0)
            {
                listBoxSimilar.Visible = true;
            }
            else
            {
                int space = ClientSize.Height - (btnOk.Top + btnOk.Height);
                btnOk.Top = listBoxSimilar.Top;
                btnCancel.Top = btnOk.Top;
                Height = btnOk.Top + (btnOk.Height) + space + (Height - ClientSize.Height);
            }

            lblSimilar.Visible = listBoxSimilar.Visible;
        }

        private void OnListDblClick(object sender, EventArgs e)
        {
            _newObjectName.UseThisName = listBoxSimilar.Text;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}