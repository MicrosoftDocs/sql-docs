'<snippetrmo_vb_BusinessLogicCode>
Imports System
Imports System.Text
Imports System.Data
Imports System.Data.Common
Imports Microsoft.SqlServer.Replication.BusinessLogicSupport

Namespace Microsoft.Samples.SqlServer.BusinessLogicHandler
    Public Class OrderEntryBusinessLogicHandler
        Inherits BusinessLogicModule

        ' Variables to hold server names.
        Private publisherName As String
        Private subscriberName As String

        ' Implement the Initialize method to get publication 
        ' and subscription information.
        Public Overrides Sub Initialize( _
        ByVal publisher As String, _
        ByVal subscriber As String, _
        ByVal distributor As String, _
        ByVal publisherDB As String, _
        ByVal subscriberDB As String, _
        ByVal articleName As String _
      )
            ' Set the Publisher and Subscriber names.
            publisherName = publisher
            subscriberName = subscriber
        End Sub

        ' Declare what types of row changes, conflicts, or errors to handle.
        Public Overrides ReadOnly Property HandledChangeStates() As ChangeStates
            Get
                ' Handle Subscriber inserts, updates and deletes.
                Return (ChangeStates.SubscriberInserts Or _
                 ChangeStates.SubscriberUpdates Or ChangeStates.SubscriberDeletes)
            End Get
        End Property

        Public Overrides Function InsertHandler(ByVal insertSource As SourceIdentifier, _
        ByVal insertedDataSet As DataSet, ByRef customDataSet As DataSet, _
        ByRef historyLogLevel As Integer, ByRef historyLogMessage As String) _
        As ActionOnDataChange

            If insertSource = SourceIdentifier.SourceIsSubscriber Then
                ' Build a line item in the audit message to log the Subscriber insert.
                Dim AuditMessage As StringBuilder = New StringBuilder()
                AuditMessage.Append(String.Format("A new order was entered at {0}. " + _
                 "The SalesOrderID for the order is :", subscriberName))
                AuditMessage.Append(insertedDataSet.Tables(0).Rows(0)("SalesOrderID").ToString())
                AuditMessage.Append("The order must be shipped by :")
                AuditMessage.Append(insertedDataSet.Tables(0).Rows(0)("DueDate").ToString())

                ' Set the reference parameter to write the line to the log file.
                historyLogMessage = AuditMessage.ToString()

                ' Set the history log level to the default verbose level.
                historyLogLevel = 1

                ' Accept the inserted data in the Subscriber's data set and 
                ' apply it to the Publisher.
                Return ActionOnDataChange.AcceptData
            Else
                Return MyBase.InsertHandler(insertSource, insertedDataSet, customDataSet, _
                 historyLogLevel, historyLogMessage)
            End If
        End Function
        Public Overrides Function UpdateHandler(ByVal updateSource As SourceIdentifier, _
        ByVal updatedDataSet As DataSet, ByRef customDataSet As DataSet, _
        ByRef historyLogLevel As Integer, ByRef historyLogMessage As String) _
        As ActionOnDataChange

            If updateSource = SourceIdentifier.SourceIsPublisher Then
                ' Build a line item in the audit message to log the Subscriber update.
                Dim AuditMessage As StringBuilder = New StringBuilder()
                AuditMessage.Append(String.Format("An existing order was updated at {0}. " + _
                 "The SalesOrderID for the order is ", subscriberName))
                AuditMessage.Append(updatedDataSet.Tables(0).Rows(0)("SalesOrderID").ToString())
                AuditMessage.Append("The order must now be shipped by :")
                AuditMessage.Append(updatedDataSet.Tables(0).Rows(0)("DueDate").ToString())

                ' Set the reference parameter to write the line to the log file.
                historyLogMessage = AuditMessage.ToString()
                ' Set the history log level to the default verbose level.
                historyLogLevel = 1

                ' Accept the updated data in the Subscriber's data set and apply it to the Publisher.
                Return ActionOnDataChange.AcceptData
            Else
                Return MyBase.UpdateHandler(updateSource, updatedDataSet, _
                 customDataSet, historyLogLevel, historyLogMessage)
            End If
        End Function
        Public Overrides Function DeleteHandler(ByVal deleteSource As SourceIdentifier, _
        ByVal deletedDataSet As DataSet, ByRef historyLogLevel As Integer, _
         ByRef historyLogMessage As String) As ActionOnDataDelete
            If deleteSource = SourceIdentifier.SourceIsSubscriber Then
                ' Build a line item in the audit message to log the Subscriber deletes.
                ' Note that the rowguid is the only information that is 
                ' available in the dataset.
                Dim AuditMessage As StringBuilder = New StringBuilder()
                AuditMessage.Append(String.Format("An existing order was deleted at {0}. " + _
                 "The rowguid for the order is ", subscriberName))
                AuditMessage.Append(deletedDataSet.Tables(0).Rows(0)("rowguid").ToString())

                ' Set the reference parameter to write the line to the log file.
                historyLogMessage = AuditMessage.ToString()
                ' Set the history log level to the default verbose level.
                historyLogLevel = 1

                ' Accept the delete and apply it to the Publisher.
                Return ActionOnDataDelete.AcceptDelete
            Else
                Return MyBase.DeleteHandler(deleteSource, deletedDataSet, _
                 historyLogLevel, historyLogMessage)
            End If
        End Function
    End Class
End Namespace
'</snippetrmo_vb_BusinessLogicCode>