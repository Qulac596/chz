using System;
using chz.Common;
using chz.WindowsServices.DataBase;

namespace chz.WindowsServices.ThisLoadDocument.DataBase
{
    /*
     * Входящий документ
     */
    public class IncomeDocument : DomainObject
    {
        public string RequestId { get; set; }   
                                     // Уникальный идентификатор запроса
        public string DocumentId { get; set; }       // Уникальный идентификатор документа
        public DateTime? Date { get; set; }          // Дата получения
        public DateTime? ProcessedDate { get; set; } // Дата обработки документа
        public string Sender { get; set; }           // Отправитель документа
        public string Receiver { get; set; }         // Получатель документа
        public string SysId { get; set; }            // Идентификатор субъекта обращения в «ИС "Маркировка". МДЛП»
        public int? DocType { get; set; }            // Тип документа
        public MDLPDocumentStatus Status { get; set; }// Статус документа
        public int? FileUploadtype { get; set; }     // Тип загрузки в систему
        public string Version { get; set; }          // Версия документа
        public string SenderSysId { get; set; }      // Идентификатор отправителя документа в «ИС "Маркировка". МДЛП»
        public string Link { get; set; }             // Ссылка на документ для загрузки
        public string Content { get; set; }   
        
        // Содержимое
        public ServiceIncomeDocumentStatus IncomeDocumentStatus { get; set; }// Статус документа в службе

        public override bool Equals(object obj)
        {
            var inconmeDocument = obj as IncomeDocument;

            if (inconmeDocument == null)
            {
                return false;
            }

            return inconmeDocument.DocumentId == DocumentId;
        }

        public override int GetHashCode()
        {
            return DocumentId.GetHashCode();
        }
    }
}
