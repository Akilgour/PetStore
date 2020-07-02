using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PetStore.Blazor.WASM.Shared.Models
{
    //For more information on IEditableObject see
    //https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.ieditableobject?redirectedfrom=MSDN&view=netcore-3.1

    public class StockDeliveryCreate : IEditableObject 
    {
        private struct StockDeliveryCreateData
        {
            internal string _name;
            internal int? _quantity;
        }

        private StockDeliveryCreateData _data;
        private StockDeliveryCreateData _backupData;
        private bool _inTransaction = false;

        public void BeginEdit()
        {
            if (!_inTransaction)
            {
                _backupData = _data;
                _inTransaction = true;
            }
        }

        public void CancelEdit()
        {
            if (_inTransaction)
            {
                _data = _backupData;
                _inTransaction = false;
            }
        }

        public void EndEdit()
        {
            if (_inTransaction)
            {
                _backupData = new StockDeliveryCreateData();
                _inTransaction = false;
            }
        }


        public bool IsValid()
        {
            return !Validate().Any();
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var vResults = new List<ValidationResult>();

            var vc = new ValidationContext(
                instance: this,
                serviceProvider: null,
                items: null);

            var isValid = Validator.TryValidateObject(
                instance: vc.ObjectInstance,
                validationContext: vc,
                validationResults: vResults,
                validateAllProperties: true);

            if (!isValid)
            {
                foreach (var validationResult in vResults)
                {
                    yield return validationResult;
                }
            }

            yield break;
        }


        public StockDeliveryCreate()
        {
            _data = new StockDeliveryCreateData();
        }

        [Required(ErrorMessage = "Name is required.")]
        public string Name
        {
            get
            {
                return _data._name;
            }
            set
            {
                this._data._name = value;
            }
        }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int? Quantity
        {
            get
            {
                return _data._quantity;
            }
            set
            {
                this._data._quantity = value;
            }
        }
    }
}