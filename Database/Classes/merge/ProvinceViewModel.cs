﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FAD3.Database.Classes.merge
{
    public class ProvinceViewModel
    {

        public ObservableCollection<Province> ProvinceCollection { get; set; }
        private ProvinceRepository Provinces { get; set; }



        public ProvinceViewModel(FADEntities fadEntities)
        {
            Provinces = new ProvinceRepository(fadEntities);
            ProvinceCollection = new ObservableCollection<Province>(Provinces.Provinces);
            ProvinceCollection.CollectionChanged += Provinces_CollectionChanged;
        }
        public List<Province> GetAllProvinces()
        {
            return ProvinceCollection.ToList();
        }
        public bool CanDeleteEntity(Province p)
        {
            return false;
        }
        public bool ProvinceNameExists(string provinceName)
        {
            foreach (Province p in ProvinceCollection)
            {
                if (p.ProvinceName == provinceName)
                {
                    return true;
                }
            }
            return false;
        }
        public Province GetProvince(int provinceID)
        {
            return ProvinceCollection.FirstOrDefault(n => n.ProvinceID == provinceID);

        }
        private void Provinces_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        int newIndex = e.NewStartingIndex;
                        Provinces.Add(ProvinceCollection[newIndex]);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    {
                        List<Province> tempListOfRemovedItems = e.OldItems.OfType<Province>().ToList();
                        Provinces.Delete(tempListOfRemovedItems[0].ProvinceID);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    {
                        List<Province> tempList = e.NewItems.OfType<Province>().ToList();
                        Provinces.Update(tempList[0]);      // As the IDs are unique, only one row will be effected hence first index only
                    }
                    break;
            }
        }

        public int Count
        {
            get { return ProvinceCollection.Count; }
        }

        public void AddRecordToRepo(Province p)
        {
            if (p == null)
                throw new ArgumentNullException("Error: The argument is Null");
            ProvinceCollection.Add(p);
        }

        public void UpdateRecordInRepo(Province p)
        {
            if (p.ProvinceID == 0)
                throw new Exception("Error: ID cannot be zero");

            int index = 0;
            while (index < ProvinceCollection.Count)
            {
                if (ProvinceCollection[index].ProvinceID == p.ProvinceID)
                {
                    ProvinceCollection[index] = p;
                    break;
                }
                index++;
            }
        }

        public void DeleteRecordFromRepo(int id)
        {
            if (id == 0)
                throw new Exception("Record ID cannot be null");

            int index = 0;
            while (index < ProvinceCollection.Count)
            {
                if (ProvinceCollection[index].ProvinceID == id)
                {
                    ProvinceCollection.RemoveAt(index);
                    break;
                }
                index++;
            }
        }


        public bool EntityValidated(Dictionary<string, string> formValues, out List<string> messages)
        {
            messages = new List<string>();
            return false;
        }
        public bool EntityValidated(Province p, out List<string> messages, bool isNew = false, string oldName = "")
        {

            messages = new List<string>();

            if (p.ProvinceName.Length < 3)
                messages.Add("Province name must be at least 3 characters long");

            if (isNew && p.ProvinceName.Length > 0 && ProvinceNameExists(p.ProvinceName))
                messages.Add("Province name already used");

            if (!isNew && p.ProvinceName.Length > 0
                 && oldName != p.ProvinceName
                && ProvinceNameExists(p.ProvinceName))
                messages.Add("Province name already used");

            return messages.Count == 0;
        }
    }

}
