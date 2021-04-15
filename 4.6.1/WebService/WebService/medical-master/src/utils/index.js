export function validateControl(value, validation) {
   if (!validation) {
      return true
   }

   let isValid = true

   if (validation.required) {
      isValid = value.trim() !== '' && isValid
   }

   if (validation.minLength) {
      isValid = value.length >= validation.minLength && isValid
   }

   return isValid
}

export function getYears(startYear) {
   var currentYear = new Date().getFullYear(),
      years = []
   startYear = startYear || 1980

   while (startYear <= currentYear) {
      let year = startYear++
      years.push({ name: year, value: year })
   }

   return years.reverse()
}

export function formatDate(date) {
   const formatDate = new Date(date)

   const result = formatDate.toLocaleDateString({
      month: '2-digit',
      year: 'numeric',
      day: '2-digit',
   })

   return result
}

export function isEmptyObject(obj) {
   for (var i in obj) {
      if (obj.hasOwnProperty(i)) {
         return false
      }
   }
   return true
}

export function clearForm(form) {
   const formCopy = { ...form }
   formCopy.isFormValid = false

   Object.keys(formCopy.formControls).forEach((item) => {
      const itemForm = formCopy.formControls[item]
      itemForm.value = ''
      itemForm.valid = false
      itemForm.touched = false
   })

   return formCopy
}
