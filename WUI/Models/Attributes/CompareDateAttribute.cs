using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WUI.Models.Attributes
{
    public class CompareDateAttribute : ValidationAttribute
    {
        #region Propriétés

        private bool _dateBefore;
        /// <summary>
        /// Par défaut, on considère que la date fournit doit être postérieure à la date saisie
        /// </summary>
        public bool DateBefore
        {
            get
            {
                return _dateBefore;
            }
            set
            {
                _dateBefore = value;
            }
        }

        /// <summary>
        /// Nom de la propriété de l'autre date
        /// </summary>
        /// <remarks>ATTENTION, la propriété doit être de type DateTime</remarks>
        private string OtherDateField { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="otherDateField">Nom de la propriété de l'autre date</param>
        public CompareDateAttribute(string otherDateField)
        {
            OtherDateField = otherDateField;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Teste si la date est bien postérieure/antérieure à l'autre date
        /// </summary>
        /// <param name="value">La date saisie</param>
        /// <param name="validationContext">Le contexte de validation</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime earlierDate;
            DateTime laterDate;
            try
            {
                // Récupération de la date saisie
                earlierDate = (DateTime)value;
                // Récupération de l'autre date
                laterDate = (DateTime)validationContext.ObjectType.GetProperty(OtherDateField).GetValue(validationContext.ObjectInstance, null);
            }
            catch (Exception ex)
            {
                return new ValidationResult("La propriété définie n'est pas de type DateTime");
            }

            // On réalise le contrôle
            if (DateBefore)
            {
                if (earlierDate <= laterDate)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("La date saisie doit être antérieure à la date de fin");
                }
            }
            else
            {
                if (earlierDate >= laterDate)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("La date saisie doit être postérieure à la date de début");
                }
            }
        }

        #endregion
    }
}