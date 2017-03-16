//<snippetrmo_BusinessLogicCode>
using System;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.SqlServer.Replication.BusinessLogicSupport;
using Microsoft.Samples.SqlServer.BusinessLogicHandler;

namespace Microsoft.Samples.SqlServer.BusinessLogicHandler
{
	public class OrderEntryBusinessLogicHandler :
	  Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule
	{
		// Variables to hold server names.
		private string publisherName;
		private string subscriberName;

		public OrderEntryBusinessLogicHandler()
		{
		}

		// Implement the Initialize method to get publication 
		// and subscription information.
		public override void Initialize(
			string publisher,
			string subscriber,
			string distributor,
			string publisherDB,
			string subscriberDB,
			string articleName)
		{
			// Set the Publisher and Subscriber names.
			publisherName = publisher;
			subscriberName = subscriber;
		}

		// Declare what types of row changes, conflicts, or errors to handle.
		override public ChangeStates HandledChangeStates
		{
			get
			{
				// Handle Subscriber inserts, updates and deletes.
				return ChangeStates.SubscriberInserts |
				  ChangeStates.SubscriberUpdates | ChangeStates.SubscriberDeletes;
			}
		}

		public override ActionOnDataChange InsertHandler(SourceIdentifier insertSource,
		  DataSet insertedDataSet, ref DataSet customDataSet, ref int historyLogLevel,
		  ref string historyLogMessage)
		{
			if (insertSource == SourceIdentifier.SourceIsSubscriber)
			{
				// Build a line item in the audit message to log the Subscriber insert.
				StringBuilder AuditMessage = new StringBuilder();
				AuditMessage.Append(String.Format("A new order was entered at {0}. " +
				  "The SalesOrderID for the order is :", subscriberName));
				AuditMessage.Append(insertedDataSet.Tables[0].Rows[0]["SalesOrderID"].ToString());
				AuditMessage.Append("The order must be shipped by :");
				AuditMessage.Append(insertedDataSet.Tables[0].Rows[0]["DueDate"].ToString());

				// Set the reference parameter to write the line to the log file.
				historyLogMessage = AuditMessage.ToString();
				
				// Set the history log level to the default verbose level.
				historyLogLevel = 1;

				// Accept the inserted data in the Subscriber's data set and 
				// apply it to the Publisher.
				return ActionOnDataChange.AcceptData;
			}
			else
			{
				return base.InsertHandler(insertSource, insertedDataSet, ref customDataSet,
				  ref historyLogLevel, ref historyLogMessage);
			}
		}

		public override ActionOnDataChange UpdateHandler(SourceIdentifier updateSource,
		  DataSet updatedDataSet, ref DataSet customDataSet, ref int historyLogLevel,
		  ref string historyLogMessage)
		{
			if (updateSource == SourceIdentifier.SourceIsPublisher)
			{
				// Build a line item in the audit message to log the Subscriber update.
				StringBuilder AuditMessage = new StringBuilder();
				AuditMessage.Append(String.Format("An existing order was updated at {0}. " +
				  "The SalesOrderID for the order is ", subscriberName));
				AuditMessage.Append(updatedDataSet.Tables[0].Rows[0]["SalesOrderID"].ToString());
				AuditMessage.Append("The order must now be shipped by :");
				AuditMessage.Append(updatedDataSet.Tables[0].Rows[0]["DueDate"].ToString());

				// Set the reference parameter to write the line to the log file.
				historyLogMessage = AuditMessage.ToString();
				// Set the history log level to the default verbose level.
				historyLogLevel = 1;

				// Accept the updated data in the Subscriber's data set and apply it to the Publisher.
				return ActionOnDataChange.AcceptData;
			}
			else
			{
				return base.UpdateHandler(updateSource, updatedDataSet,
				  ref customDataSet, ref historyLogLevel, ref historyLogMessage);
			}
		}

		public override ActionOnDataDelete DeleteHandler(SourceIdentifier deleteSource,
		  DataSet deletedDataSet, ref int historyLogLevel, ref string historyLogMessage)
		{
			if (deleteSource == SourceIdentifier.SourceIsSubscriber)
			{
				// Build a line item in the audit message to log the Subscriber deletes.
				// Note that the rowguid is the only information that is 
				// available in the dataset.
				StringBuilder AuditMessage = new StringBuilder();
				AuditMessage.Append(String.Format("An existing order was deleted at {0}. " +
				  "The rowguid for the order is ", subscriberName));
				AuditMessage.Append(deletedDataSet.Tables[0].Rows[0]["rowguid"].ToString());

				// Set the reference parameter to write the line to the log file.
				historyLogMessage = AuditMessage.ToString();
				// Set the history log level to the default verbose level.
				historyLogLevel = 1;

				// Accept the delete and apply it to the Publisher.
				return ActionOnDataDelete.AcceptDelete;
			}
			else
			{
				return base.DeleteHandler(deleteSource, deletedDataSet,
				  ref historyLogLevel, ref historyLogMessage);
			}
		}
	}
}
//</snippetrmo_BusinessLogicCode>
