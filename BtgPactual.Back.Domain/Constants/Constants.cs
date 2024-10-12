using System.Diagnostics.CodeAnalysis;

namespace BtgPactual.Back.Domain.Constants
{
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        public static class Formats
        {
            public const string DateTime = "yyyy-MM-dd HH:mm";
        }

        public static class Collections
        {
            public const string Customers = "Customers";
            public const string Funds = "Funds";
        }

        public static class Notifications
        {
            public const string Subject = "Notificacion Transaccion";
            public const string NotificationSent = "Notificacion enviada correctamente";
            public const string NotificationNotSent = "Error Enviando notificacion";
            public const string TransactionNotFound = "Transaccion no encontrada";
            public const string FundNotFound = "Fondo no encontrado";
            public const string Template = "Transaccion realizada con el fondo {0}, por un monto de {1}, transaccion de tipo {2}, fecha {3}";
        }

        public static class TransactionsFund
        {
            public const string CustomerNotFound = "Usuario no encontrado";
            public const string FundNotFound = "Fondo no encontrado";
            public const string FundsNotFound = "No se encontro ningun fondo";
            public const string TransactionsFound = "Historial de transacciones cargada";
            public const string DetailTransactionsFound = "Detalles de transacciones cargados";
        }

        public static class SubscribeFund
        {
            public const string NoBalanceAvailable = "No tiene saldo disponible para vincularse al fondo {0}";
            public const string NotMinimumAmount = "El Monto minimo para vincularse al fondo {0}, es {1}";
            public const string Subscribed = "Vinculacion exitosa al fondo {0}";
            public const string Error = "Error al vincularse al fondo {0}";
        }

        public static class UnsubscribeFund
        {
            public const string TransactionNotFound = "Transaccion con id {0} no encontrada o no hace parte del fondo {1}";
            public const string TransactionIsNotOpening = "Transaccion con id {0} no es una vinculacion";
            public const string TransactionAlreadyUnsubscribed = "La transaccion {0} no se encuetra vinculada";
            public const string Unsubscribed = "Desvinculacion exitosa al fondo {0}";
            public const string Error = "Error al desvincularse al fondo {0}";
        }
    }
}
