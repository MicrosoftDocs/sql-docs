#region Using directives

using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using Microsoft.SqlServer.Replication;

#endregion

namespace Microsoft.Samples.SqlServer.Replication.Rmo
{
	class RMOTestEvelope
	{
		// Set the global testing defaults
		private static string publisherInstance = Environment.MachineName;
        private static string distributorInstance = Environment.MachineName;
        private static string subscriberInstance = Environment.MachineName;
        private static string loginName = RMOHowTo.Properties.Settings.Default.LoginName;
        private static string password = RMOHowTo.Properties.Settings.Default.Password; 
        private static string hostName = RMOHowTo.Properties.Settings.Default.HostName;

        [STAThread]
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				throw new ApplicationException("You must specify a command line parameter");
			}

			args[0] = args[0].ToUpper();

			try
			{
				// Create the sub DB if not exists.
				Initialize();

				switch (args[0])
				{
					case "-CDP":
					case "/CDP":
						ConfigDistPublisher();
						break;

					case "-MDP":
					case "/MDP":
						if (args.Length == 2)
						{
							ModifyDistPublisher(args[1]);
						}
						else
						{
							throw new ApplicationException("You must supply the new distributor password.");
						}
						break;

					case "-RDP":
					case "/RDP":
						RemoveDistPublisher();
						break;

					case "-RPF":
					case "/RPF":
						RemoveDistPublisherForce();
						break;

					case "-CMP":
					case "/CMP":
						CreateMergePublication(loginName, password);
						break;

					case "-CTP":
					case "/CTP":
                        CreateTranPublication(loginName, password);
						break;

					case "-HMP":
					case "/HMP":
						ChangeMergePublication();
						break;

					case "-HTP":
					case "/HTP":
						ChangeTranPublicationCached();
						break;

					case "-RTP":
					case "/RTP":
						RemoveTranPublication();
						break;

					case "-RMP":
					case "/RMP":
						RemoveMergePublication();
						break;

					case "-CTA":
					case "/CTA":
						CreateTranArticle();
						break;

					case "-CMA":
					case "/CMA":
						CreateMergeArticle();
						break;

					case "-CML":
					case "/CML":
                        CreateMergePullSub(loginName, password);
						break;

					case "-CMS":
					case "/CMS":
                        CreateMergePushSub(loginName, password);
						break;

					case "-CTS":
					case "/CTS":
                        CreateTranPushSub(loginName, password);
						break;

					case "-CTL":
					case "/CTL":
                        CreateTranPullSub(loginName, password);
						break;

					case "-DTL":
					case "/DTL":
						RemoveTranPullSub();
						break;

					case "-DTS":
					case "/DTS":
						RemoveTranPushSub();
						break;
					
					case "-DML":
					case "/DML":
						 RemoveMergePullSub();
						break;

					case "-DMS":
					case "/DMS":
						RemoveMergePushSub();
						break;
					
					case "-STL":
					case "/STL":
						SyncTranPullSub();
						break;

					case "-STS":
					case "/STS":
						SyncTranPushSub();
						break;

					case "-SML":
					case "/SML":
						SyncMergePullSub();
						break;

					case "-SMS":
					case "/SMS":
						SyncMergePushSub();
						break;

					case "-SMLJ":
					case "/SMLJ":
						SyncMergePullSubWithJob();
						break;

					case "-SMSJ":
					case "/SMSJ":
						SyncMergePushSubWithJob();
						break;

					case "-SMLW":
					case "/SMLW":
                        SyncMergePullSubNoJobWebSync(loginName, password);
						break;

					case "-STSJ":
					case "/STSJ":
						SyncTranPushSubWithJob();
						break;

					case "-STLJ":
					case "/STLJ":
						SyncTranPullSubWithJob();
						break;
					
					case "-CWS":
					case "/CWS":
                        ConfigureWebSync(loginName, password);
						break;

					case "-CMLW":
					case "/CMLW":
                        CreateMergePullSubWebSync(loginName, password);
						break;

					case "-CMLN":
					case "/CMLN":
						CreateMergePullSubNoJob();
						break;

					case "-SSJ":
					case "/SSJ":
						GeneratedSnapshotWithJob();
						break;
					
					case "-GTS":
					case "/GTS":
						GenerateTranSnapshot();
						break;

                    case "-GMS":
                    case "/GMS":
                        GenerateMergeSnapshot();
                        break;
                    
                    case "-GFS":
					case "/GFS":
						GeneratedFilteredSnapshot(hostName);
						break;

					case "-CP":
					case "/CP":
						CreateMergePartition(hostName);
						break;

					case "-RBL":
					case "/RBL":
						RegisterBLH();
						break;
                
                    case "-HMAB":
                    case "/HMAB":
                        ChangeMergeArticle_BLH();
                        break;

					case "-RTL":
					case "/RTL":
						ReinitTranPullSub();
						break;

					case "-RML":
					case "/RML":
						ReinitMergePullSub_WithUpload();
						break;

					case "-CSP":
					case "/CSP":
                        ChangeServerPasswords(loginName, password);
						break;

					case "-VTP":
					case "/VTP":
						ValidateTranPublication();
						break;

					case "-VMS":
					case "/VMS":
						ValidateMergeSubscription();
						break;

                    case "-CLR":
                    case "/CLR":
                        CreateLogicalRecord();
                        break;

                    case "-CMLA":
                    case "/CMLA":
                        CreateMergePullSubWebSyncAnon(loginName, password);
                        break;

                    case "/?":
					case "-?":
					case "?":
						OutputParams();
						break;

					default:
						Console.WriteLine("bad parameter passed");
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("The following error occured: " + ex.ToString());
			}
			finally
			{
				//
			}
		}
		static public void Initialize()
		{
			string subscriptionDbName = "AdventureWorks2012Replica";
			ServerConnection conn = new ServerConnection(publisherInstance);
			Database newDatabase;
			Server subServer = new Server(conn);

			// Create the subscription database.
			if (subServer.Databases.Contains(subscriptionDbName) == false)
			{
				newDatabase = new Database(subServer, subscriptionDbName);
				newDatabase.Create();
			}
		}
		static public void ConfigDistPublisher()
		{
			//<snippetrmo_AddDistPub>
			// Set the server and database names
			string distributionDbName = "distribution";
			string publisherName = publisherInstance;
			string publicationDbName = "AdventureWorks2012";

			DistributionDatabase distributionDb;
			ReplicationServer distributor;
			DistributionPublisher publisher;
			ReplicationDatabase publicationDb;

			// Create a connection to the server using Windows Authentication.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the server acting as the Distributor 
				// and local Publisher.
				conn.Connect();

				// Define the distribution database at the Distributor,
				// but do not create it now.
				distributionDb = new DistributionDatabase(distributionDbName, conn);
				distributionDb.MaxDistributionRetention = 96;
				distributionDb.HistoryRetention = 120;

				// Set the Distributor properties and install the Distributor.
				// This also creates the specified distribution database.
				distributor = new ReplicationServer(conn);
				distributor.InstallDistributor((string)null, distributionDb);

				// Set the Publisher properties and install the Publisher.
				publisher = new DistributionPublisher(publisherName, conn);
				publisher.DistributionDatabase = distributionDb.Name;
				publisher.WorkingDirectory = @"\\" + publisherName + @"\repldata";
				publisher.PublisherSecurity.WindowsAuthentication = true;
				publisher.Create();

				// Enable AdventureWorks2012 as a publication database.
				publicationDb = new ReplicationDatabase(publicationDbName, conn);

				publicationDb.EnabledTransPublishing = true;
				publicationDb.EnabledMergePublishing = true;
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("An error occured when installing distribution and publishing.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_AddDistPub>
		}
		static public void ModifyDistPublisher(string password)
		{
			//<snippetrmo_ChangeDistPub>
			// Set the Distributor and distribution database names.
			string distributionDbName = "distribution";
			string distributorName = publisherInstance;

			ReplicationServer distributor;
			DistributionDatabase distributionDb;

			// Create a connection to the Distributor using Windows Authentication.
			ServerConnection conn = new ServerConnection(distributorName);

			try
			{
				// Open the connection. 
				conn.Connect();

				distributor = new ReplicationServer(conn);

				// Load Distributor properties, if it is installed.
				if (distributor.LoadProperties())
				{
					// Password supplied at runtime.
					distributor.ChangeDistributorPassword(password);
					distributor.AgentCheckupInterval = 5;

					// Save changes to the Distributor properties.
					distributor.CommitPropertyChanges();
				}
				else
				{
					throw new ApplicationException(
						String.Format("{0} is not a Distributor.", publisherInstance));
				}

				// Create an object for the distribution database 
				// using the open Distributor connection.
				distributionDb = new DistributionDatabase(distributionDbName, conn);

				// Change distribution database properties.
				if (distributionDb.LoadProperties())
				{
					// Change maximum retention period to 48 hours and history retention 
					// period to 24 hours.
					distributionDb.MaxDistributionRetention = 48;
					distributionDb.HistoryRetention = 24;

					// Save changes to the distribution database properties.
					distributionDb.CommitPropertyChanges();
				}
				else
				{
					// Do something here if the distribution database does not exist.
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here. 
				throw new ApplicationException("An error occured when changing Distributor " +
					" or distribution database properties.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_ChangeDistPub>
		}
		static public void RemoveDistPublisher()
		{
			//<snippetrmo_DropDistPub>
			// Set the Distributor and publication database names.
			// Publisher and Distributor are on the same server instance.
			string publisherName = publisherInstance;
			string distributorName = publisherInstance;
			string distributionDbName = "distribution";
			string publicationDbName = "AdventureWorks2012";

			// Create connections to the Publisher and Distributor
			// using Windows Authentication.
			ServerConnection publisherConn = new ServerConnection(publisherName);
			ServerConnection distributorConn = new ServerConnection(distributorName);

			// Create the objects we need.
			ReplicationServer distributor =
				new ReplicationServer(distributorConn);
			DistributionPublisher publisher;
			DistributionDatabase distributionDb =
				new DistributionDatabase(distributionDbName, distributorConn);
			ReplicationDatabase publicationDb;
			publicationDb = new ReplicationDatabase(publicationDbName, publisherConn);

			try
			{
				// Connect to the Publisher and Distributor.
				publisherConn.Connect();
				distributorConn.Connect();

				// Disable all publishing on the AdventureWorks2012 database.
				if (publicationDb.LoadProperties())
				{
					if (publicationDb.EnabledMergePublishing)
					{
						publicationDb.EnabledMergePublishing = false;
					}
					else if (publicationDb.EnabledTransPublishing)
					{
						publicationDb.EnabledTransPublishing = false;
					}
				}
				else
				{
					throw new ApplicationException(
						String.Format("The {0} database does not exist.", publicationDbName));
				}

				// We cannot uninstall the Publisher if there are still Subscribers.
				if (distributor.RegisteredSubscribers.Count == 0)
				{
					// Uninstall the Publisher, if it exists.
					publisher = new DistributionPublisher(publisherName, distributorConn);
					if (publisher.LoadProperties())
					{
						publisher.Remove(false);
					}
					else
					{
						// Do something here if the Publisher does not exist.
						throw new ApplicationException(String.Format(
							"{0} is not a Publisher for {1}.", publisherName, distributorName));
					}

					// Drop the distribution database.
					if (distributionDb.LoadProperties())
					{
						distributionDb.Remove();
					}
					else
					{
						// Do something here if the distribition DB does not exist.
						throw new ApplicationException(String.Format(
							"The distribution database '{0}' does not exist on {1}.",
							distributionDbName, distributorName));
					}

					// Uninstall the Distributor, if it exists.
					if (distributor.LoadProperties())
					{
						// Passing a value of false means that the Publisher 
						// and distribution databases must already be uninstalled,
						// and that no local databases be enabled for publishing.
						distributor.UninstallDistributor(false);
					}
					else
					{
						//Do something here if the distributor does not exist.
						throw new ApplicationException(String.Format(
							"The Distributor '{0}' does not exist.", distributorName));
					}
				}
				else
				{
					throw new ApplicationException("You must first delete all subscriptions.");
				}
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("The Publisher and Distributor could not be uninstalled", ex);
			}
			finally
			{
				publisherConn.Disconnect();
				distributorConn.Disconnect();
			}
			//</snippetrmo_DropDistPub>
		}
		static public void RemoveDistPublisherForce()
		{
			//<snippetrmo_DropDistPubForce>
			// Set the Distributor and publication database names.
			// Publisher and Distributor are on the same server instance.
			string distributorName = publisherInstance;

			// Create connections to the Distributor
			// using Windows Authentication.
			ServerConnection conn = new ServerConnection(distributorName);
			conn.DatabaseName = "master";

			// Create the objects we need.
			ReplicationServer distributor = new ReplicationServer(conn);

			try
			{
				// Connect to the Publisher and Distributor.
				conn.Connect();


				// Uninstall the Distributor, if it exists.
				// Use the force parameter to remove everthing.  
				if (distributor.IsDistributor && distributor.LoadProperties())
				{
					// Passing a value of true means that the Distributor 
					// is uninstalled even when publishing objects, subscriptions,
					// and distribution databases exist on the server.
					distributor.UninstallDistributor(true);
				}
				else
				{
					//Do something here if the distributor does not exist.
				}
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("The Publisher and Distributor could not be uninstalled", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_DropDistPubForce>
		}
		static public void CreateTranPublication(string winLogin, string winPassword)
		{
			//<snippetrmo_CreateTranPub>
			// Set the Publisher, publication database, and publication names.
			string publicationName = "AdvWorksProductTran";
			string publicationDbName = "AdventureWorks2012";
			string publisherName = publisherInstance;

			ReplicationDatabase publicationDb;
			TransPublication publication;

			// Create a connection to the Publisher using Windows Authentication.
			ServerConnection conn;
			conn = new ServerConnection(publisherName);


			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Enable the AdventureWorks2012 database for transactional publishing.
				publicationDb = new ReplicationDatabase(publicationDbName, conn);

				// If the database exists and is not already enabled, 
				// enable it for transactional publishing.
				if (publicationDb.LoadProperties())
				{
					if (!publicationDb.EnabledTransPublishing)
					{
						publicationDb.EnabledTransPublishing = true;
					}

					// If the Log Reader Agent does not exist, create it.
					if (!publicationDb.LogReaderAgentExists)
					{
						// Specify the Windows account under which the agent job runs.
						// This account will be used for the local connection to the 
						// Distributor and all agent connections that use Windows Authentication.
						publicationDb.LogReaderAgentProcessSecurity.Login = winLogin;
						publicationDb.LogReaderAgentProcessSecurity.Password = winPassword;

						// Explicitly set authentication mode for the Publisher connection
						// to the default value of Windows Authentication.
						publicationDb.LogReaderAgentPublisherSecurity.WindowsAuthentication = true;

						// Create the Log Reader Agent job.
						publicationDb.CreateLogReaderAgent();
					}
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The {0} database does not exist at {1}.",
						publicationDb, publisherName));
				}

				// Set the required properties for the transactional publication.
				publication = new TransPublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;

				// Specify a transactional publication (the default).
				publication.Type = PublicationType.Transactional;

				// Activate the publication so that we can add subscriptions.
				publication.Status = State.Active;

                // Enable push and pull subscriptions and independent Distribition Agents.
                publication.Attributes |= PublicationAttributes.AllowPull;
                publication.Attributes |= PublicationAttributes.AllowPush;
                publication.Attributes |= PublicationAttributes.IndependentAgent;

				// Specify the Windows account under which the Snapshot Agent job runs.
				// This account will be used for the local connection to the 
				// Distributor and all agent connections that use Windows Authentication.
				publication.SnapshotGenerationAgentProcessSecurity.Login = winLogin;
				publication.SnapshotGenerationAgentProcessSecurity.Password = winPassword;

				// Explicitly set the security mode for the Publisher connection
				// Windows Authentication (the default).
				publication.SnapshotGenerationAgentPublisherSecurity.WindowsAuthentication = true;

				if (!publication.IsExistingObject)
				{
					// Create the transactional publication.
					publication.Create();

					// Create a Snapshot Agent job for the publication.
					publication.CreateSnapshotAgent();
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The {0} publication already exists.", publicationName));
				}
			}

			catch (Exception ex)
			{
				// Implement custom application error handling here.
				throw new ApplicationException(String.Format(
					"The publication {0} could not be created.", publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_CreateTranPub>
		}
		static public void RemoveTranPublication()
		{
			//<snippetrmo_DropTranPub>
			// Define the Publisher, publication database, 
			// and publication names.
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksProductTran";
			string publicationDbName = "AdventureWorks2012";

			TransPublication publication;
			ReplicationDatabase publicationDb;

			// Create a connection to the Publisher 
			// using Windows Authentication.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				conn.Connect();

				// Set the required properties for the transactional publication.
				publication = new TransPublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;

				// Delete the publication, if it exists and has no subscriptions.
				if (publication.LoadProperties() && !publication.HasSubscription)
				{
					publication.Remove();
				}
				else
				{
					// Do something here if the publication does not exist
					// or has subscriptions.
					throw new ApplicationException(String.Format(
						"The publication {0} could not be deleted. " +
						"Ensure that the publication exists and that all " +
						"subscriptions have been deleted.",
						publicationName, publisherName));
				}

				// If no other transactional publications exists,
				// disable publishing on the database.
				publicationDb = new ReplicationDatabase(publicationDbName, conn);
				if (publicationDb.LoadProperties())
				{
					if (publicationDb.TransPublications.Count == 0)
					{
						publicationDb.EnabledTransPublishing = false;
					}
				}
				else
				{
					// Do something here if the database does not exist.
					throw new ApplicationException(String.Format(
						"The database {0} does not exist on {1}.",
						publicationDbName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Implement application error handling here.
				throw new ApplicationException(String.Format(
					"The publication {0} could not be deleted.",
					publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_DropTranPub>
		}
		static public void CreateMergePublication(string winLogin, string winPassword)
		{
			//<snippetrmo_CreateMergePub>
			// Set the Publisher, publication database, and publication names.
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publicationDbName = "AdventureWorks2012";

			ReplicationDatabase publicationDb;
			MergePublication publication;

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Enable the database for merge publication.				
				publicationDb = new ReplicationDatabase(publicationDbName, conn);
				if (publicationDb.LoadProperties())
				{
					if (!publicationDb.EnabledMergePublishing)
					{
						publicationDb.EnabledMergePublishing = true;
					}
				}
				else
				{
					// Do something here if the database does not exist. 
					throw new ApplicationException(String.Format(
						"The {0} database does not exist on {1}.",
						publicationDb, publisherName));
				}

				// Set the required properties for the merge publication.
				publication = new MergePublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;

                // Enable precomputed partitions.
                publication.PartitionGroupsOption = PartitionGroupsOption.True;

				// Specify the Windows account under which the Snapshot Agent job runs.
				// This account will be used for the local connection to the 
				// Distributor and all agent connections that use Windows Authentication.
				publication.SnapshotGenerationAgentProcessSecurity.Login = winLogin;
				publication.SnapshotGenerationAgentProcessSecurity.Password = winPassword;

				// Explicitly set the security mode for the Publisher connection
				// Windows Authentication (the default).
				publication.SnapshotGenerationAgentPublisherSecurity.WindowsAuthentication = true;

				// Enable Subscribers to request snapshot generation and filtering.
				publication.Attributes |= PublicationAttributes.AllowSubscriberInitiatedSnapshot;
                publication.Attributes |= PublicationAttributes.DynamicFilters;

                // Enable pull and push subscriptions.
                publication.Attributes |= PublicationAttributes.AllowPull;
                publication.Attributes |= PublicationAttributes.AllowPush;

				if (!publication.IsExistingObject)
				{
					// Create the merge publication.
					publication.Create();
					
					// Create a Snapshot Agent job for the publication.
					publication.CreateSnapshotAgent();
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The {0} publication already exists.", publicationName));
				}
			}

			catch (Exception ex)
			{
				// Implement custom application error handling here.
				throw new ApplicationException(String.Format(
					"The publication {0} could not be created.", publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_CreateMergePub>
		}
		static public void ChangeMergePublication()
		{
			//<snippetrmo_ChangeMergePub_ddl>
			// Define the server, database, and publication names
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publicationDbName = "AdventureWorks2012";

			MergePublication publication;

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Set the required properties for the publication.
				publication = new MergePublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;


				// If we can't get the properties for this merge publication, then throw an application exception.
				if (publication.LoadProperties())
				{
					// If DDL replication is currently enabled, disable it.
					if (publication.ReplicateDdl == DdlReplicationOptions.All)
					{
						publication.ReplicateDdl = DdlReplicationOptions.None;
					}
					else
					{
						publication.ReplicateDdl = DdlReplicationOptions.All;
					}
				}
				else
				{
					throw new ApplicationException(String.Format(
						"Settings could not be retrieved for the publication. " +
						"Ensure that the publication {0} exists on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Do error handling here.
				throw new ApplicationException(
					"The publication property could not be changed.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_ChangeMergePub_ddl>
		}
		static public void ChangeTranPublicationCached()
		{
			//<snippetrmo_ChangeTranPub_cached>
			// Define the server, database, and publication names
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksProductTran";
			string publicationDbName = "AdventureWorks2012";

			TransPublication publication;

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Set the required properties for the publication.
				publication = new TransPublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;

				// Explicitly enable caching of property changes on this object.
				publication.CachePropertyChanges = true;

				// If we can't get the properties for this publication, 
				// throw an application exception.
				if (publication.LoadProperties())
				{
					// Enable support for push subscriptions and disable support 
					// for pull subscriptions.
					if ((publication.Attributes & PublicationAttributes.AllowPull) != 0)
					{
						publication.Attributes ^= PublicationAttributes.AllowPull;
					}
					if ((publication.Attributes & PublicationAttributes.AllowPush) == 0)
					{
						publication.Attributes |= PublicationAttributes.AllowPush;
					}

					// Send changes to the server.
					publication.CommitPropertyChanges();
				}
				else
				{
					throw new ApplicationException(String.Format(
						"Settings could not be retrieved for the publication. " +
						"Ensure that the publication {0} exists on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Do error handling here.
				throw new ApplicationException(
					"The publication property could not be changed.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_ChangeTranPub_cached>
		}
		static public void RemoveMergePublication()
		{
			//<snippetrmo_DropMergePub>
			// Define the Publisher, publication database, 
			// and publication names.
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publicationDbName = "AdventureWorks2012";

			MergePublication publication;
			ReplicationDatabase publicationDb;

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Set the required properties for the merge publication.
				publication = new MergePublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;

				// Delete the publication, if it exists and has no subscriptions.
				if (publication.LoadProperties() && !publication.HasSubscription)
				{
					publication.Remove();
				}
				else
				{
					// Do something here if the publication does not exist
					// or has subscriptions.
					throw new ApplicationException(String.Format(
						"The publication {0} could not be deleted. " +
						"Ensure that the publication exists and that all " +
						"subscriptions have been deleted.",
						publicationName, publisherName));
				}

				// If no other merge publications exists,
				// disable publishing on the database.
				publicationDb = new ReplicationDatabase(publicationDbName, conn);
				if (publicationDb.LoadProperties())
				{
					if (publicationDb.MergePublications.Count == 0 && publicationDb.EnabledMergePublishing)
					{
						publicationDb.EnabledMergePublishing = false;
					}
				}
				else
				{
					// Do something here if the database does not exist.
					throw new ApplicationException(String.Format(
						"The database {0} does not exist on {1}.",
						publicationDbName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Implement application error handling here.
				throw new ApplicationException(String.Format(
					"The publication {0} could not be deleted.",
					publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_DropMergePub>
		}
		static public void CreateTranArticle()
		{
			//<snippetrmo_CreateTranArticles>
			// Define the Publisher, publication, and article names.
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksProductTran";
			string publicationDbName = "AdventureWorks2012";
			string articleName = "Product";
			string schemaOwner = "Production";

			TransArticle article;

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			// Create a filtered transactional articles in the following steps:
			// 1) Create the  article with a horizontal filter clause.
			// 2) Add columns to or remove columns from the article.
			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Define a horizontally filtered, log-based table article.
				article = new TransArticle();
				article.ConnectionContext = conn;
				article.Name = articleName;
				article.DatabaseName = publicationDbName;
				article.SourceObjectName = articleName;
				article.SourceObjectOwner = schemaOwner;
				article.PublicationName = publicationName;
				article.Type = ArticleOptions.LogBased;
				article.FilterClause = "DiscontinuedDate IS NULL";

				// Ensure that we create the schema owner at the Subscriber.
				article.SchemaOption |= CreationScriptOptions.Schema;

				if (!article.IsExistingObject)
				{
					// Create the article.
					article.Create();
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The article {0} already exists in publication {1}.",
						articleName, publicationName));
				}

				// Create an array of column names to remove from the article.
				String[] columns = new String[1];
				columns[0] = "DaysToManufacture";

				// Remove the column from the article.
				article.RemoveReplicatedColumns(columns);
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("The article could not be created.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_CreateTranArticles>
		}
		static public void CreateMergeArticle()
		{
			//<snippetrmo_CreateMergeArticles>
			// Define the Publisher and publication names.
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publicationDbName = "AdventureWorks2012";

			// Specify article names.
			string articleName1 = "Employee";
			string articleName2 = "SalesOrderHeader";
			string articleName3 = "SalesOrderDetail";

			// Specify join filter information.
			string filterName12 = "SalesOrderHeader_Employee";
			string filterClause12 = "Employee.EmployeeID = " +
				"SalesOrderHeader.SalesPersonID";
			string filterName23 = "SalesOrderDetail_SalesOrderHeader";
			string filterClause23 = "SalesOrderHeader.SalesOrderID = " +
				"SalesOrderDetail.SalesOrderID";

			string salesSchema = "Sales";
			string hrSchema = "HumanResources";

			MergeArticle article1 = new MergeArticle();
			MergeArticle article2 = new MergeArticle();
			MergeArticle article3 = new MergeArticle();
			MergeJoinFilter filter12 = new MergeJoinFilter();
			MergeJoinFilter filter23 = new MergeJoinFilter();

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			// Create three merge articles that are horizontally partitioned
			// using a parameterized row filter on Employee.EmployeeID, which is 
			// extended to the two other articles using join filters. 
			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Create each article. 
				// For clarity, each article is defined separately. 
				// In practice, iterative structures and arrays should 
				// be used to efficiently create multiple articles.

				// Set the required properties for the Employee article.
				article1.ConnectionContext = conn;
				article1.Name = articleName1;
				article1.DatabaseName = publicationDbName;
				article1.SourceObjectName = articleName1;
				article1.SourceObjectOwner = hrSchema;
				article1.PublicationName = publicationName;
				article1.Type = ArticleOptions.TableBased;

				// Define the parameterized filter clause based on Hostname.
				article1.FilterClause = "Employee.LoginID = HOST_NAME()";

				// Set the required properties for the SalesOrderHeader article.
				article2.ConnectionContext = conn;
				article2.Name = articleName2;
				article2.DatabaseName = publicationDbName;
				article2.SourceObjectName = articleName2;
				article2.SourceObjectOwner = salesSchema;
				article2.PublicationName = publicationName;
				article2.Type = ArticleOptions.TableBased;

				// Set the required properties for the SalesOrderDetail article.
				article3.ConnectionContext = conn;
				article3.Name = articleName3;
				article3.DatabaseName = publicationDbName;
				article3.SourceObjectName = articleName3;
				article3.SourceObjectOwner = salesSchema;
				article3.PublicationName = publicationName;
				article3.Type = ArticleOptions.TableBased;

				if (!article1.IsExistingObject) article1.Create();
				if (!article2.IsExistingObject) article2.Create();
				if (!article3.IsExistingObject) article3.Create();

				// Select published columns for SalesOrderHeader.
				// Create an array of column names to vertically filter out.
				// In this example, only one column is removed.
				String[] columns = new String[1];

				columns[0] = "CreditCardApprovalCode";

				// Remove the column.
				article2.RemoveReplicatedColumns(columns);

				// Define a merge filter clauses that filter 
				// SalesOrderHeader based on Employee and 
				// SalesOrderDetail based on SalesOrderHeader. 

				// Parent article.
				filter12.JoinArticleName = articleName1;
				// Child article.
				filter12.ArticleName = articleName2;
				filter12.FilterName = filterName12;
				filter12.JoinUniqueKey = true;
				filter12.FilterTypes = FilterTypes.JoinFilter;
				filter12.JoinFilterClause = filterClause12;

				// Add the join filter to the child article.
				article2.AddMergeJoinFilter(filter12);

				// Parent article.
				filter23.JoinArticleName = articleName2;
				// Child article.
				filter23.ArticleName = articleName3;
				filter23.FilterName = filterName23;
				filter23.JoinUniqueKey = true;
				filter23.FilterTypes = FilterTypes.JoinFilter;
				filter23.JoinFilterClause = filterClause23;

				// Add the join filter to the child article.
				article3.AddMergeJoinFilter(filter23);
			}
			catch (Exception ex)
			{
				// Do error handling here and rollback the transaction.
				throw new ApplicationException(
					"The filtered articles could not be created", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_CreateMergeArticles>
		}
		static public void CreateTranPushSub(string winLogin, string winPassword)
		{
			//<snippetrmo_CreateTranPushSub>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksProductTran";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			//Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(subscriberName);

			// Create the objects that we need.
			TransPublication publication;
			TransSubscription subscription;

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Ensure that the publication exists and that 
				// it supports push subscriptions.
				publication = new TransPublication();
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;
				publication.ConnectionContext = conn;

				if (publication.IsExistingObject)
				{
					if ((publication.Attributes & PublicationAttributes.AllowPush) == 0)
					{
						publication.Attributes |= PublicationAttributes.AllowPush;
					}

					// Define the push subscription.
					subscription = new TransSubscription();
					subscription.ConnectionContext = conn;
					subscription.SubscriberName = subscriberName;
					subscription.PublicationName = publicationName;
					subscription.DatabaseName = publicationDbName;
					subscription.SubscriptionDBName = subscriptionDbName;

					// Specify the Windows login credentials for the Distribution Agent job.
					subscription.SynchronizationAgentProcessSecurity.Login = winLogin;
					subscription.SynchronizationAgentProcessSecurity.Password = winPassword;

                    // By default, subscriptions to transactional publications are synchronized 
                    // continuously, but in this case we only want to synchronize on demand.
                    subscription.AgentSchedule.FrequencyType = ScheduleFrequencyType.OnDemand;
 
					// Create the push subscription.
					subscription.Create();
				}
				else
				{
					// Do something here if the publication does not exist.
					throw new ApplicationException(String.Format(
						"The publication '{0}' does not exist on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be created.", publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_CreateTranPushSub>
		}
		static public void CreateTranPullSub(string winLogin, string winPassword)
		{
			//<snippetrmo_CreateTranPullSub>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksProductTran";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			//Create connections to the Publisher and Subscriber.
			ServerConnection subscriberConn = new ServerConnection(subscriberName);
			ServerConnection publisherConn = new ServerConnection(publisherName);

			// Create the objects that we need.
			TransPublication publication;
			TransPullSubscription subscription;

			try
			{
				// Connect to the Publisher and Subscriber.
				subscriberConn.Connect();
                publisherConn.Connect();

				// Ensure that the publication exists and that 
				// it supports pull subscriptions.
				publication = new TransPublication();
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;
				publication.ConnectionContext = publisherConn;

				if (publication.IsExistingObject)
				{
					if ((publication.Attributes & PublicationAttributes.AllowPull) == 0)
					{
						publication.Attributes |= PublicationAttributes.AllowPull;
					}

					// Define the pull subscription.
					subscription = new TransPullSubscription();
					subscription.ConnectionContext = subscriberConn;
					subscription.PublisherName = publisherName;
					subscription.PublicationName = publicationName;
					subscription.PublicationDBName = publicationDbName;
					subscription.DatabaseName = subscriptionDbName;

					// Specify the Windows login credentials for the Distribution Agent job.
					subscription.SynchronizationAgentProcessSecurity.Login = winLogin;
					subscription.SynchronizationAgentProcessSecurity.Password = winPassword;

					// Make sure that the agent job for the subscription is created.
					subscription.CreateSyncAgentByDefault = true;

                    // By default, subscriptions to transactional publications are synchronized 
                    // continuously, but in this case we only want to synchronize on demand.
                    subscription.AgentSchedule.FrequencyType = ScheduleFrequencyType.OnDemand;

                    // Create the pull subscription at the Subscriber.
					subscription.Create();

					Boolean registered = false;

					// Verify that the subscription is not already registered.
					foreach (TransSubscription existing
						in publication.EnumSubscriptions())
					{
						if (existing.SubscriberName == subscriberName
							&& existing.SubscriptionDBName == subscriptionDbName)
						{
							registered = true;
						}
					}
					if (!registered)
					{
						// Register the subscription with the Publisher.
						publication.MakePullSubscriptionWellKnown(
							subscriberName, subscriptionDbName,
							SubscriptionSyncType.Automatic,
							TransSubscriberType.ReadOnly);
					}
				}
				else
				{
					// Do something here if the publication does not exist.
					throw new ApplicationException(String.Format(
						"The publication '{0}' does not exist on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be created.", publicationName), ex);
			}
			finally
			{
				subscriberConn.Disconnect();
				publisherConn.Disconnect();
			}
			//</snippetrmo_CreateTranPullSub>
		}
		static public void CreateMergePullSub(string winLogin, string winPassword)
		{
			//<snippetrmo_CreateMergePullSub>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";
			string hostname = @"adventure-works\garrett1";

			//Create connections to the Publisher and Subscriber.
			ServerConnection subscriberConn = new ServerConnection(subscriberName);
			ServerConnection publisherConn = new ServerConnection(publisherName);

			// Create the objects that we need.
			MergePublication publication;
			MergePullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				subscriberConn.Connect();

				// Ensure that the publication exists and that 
				// it supports pull subscriptions.
				publication = new MergePublication();
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;
				publication.ConnectionContext = publisherConn;

				if (publication.LoadProperties())
				{
					if ((publication.Attributes & PublicationAttributes.AllowPull) == 0)
					{
						publication.Attributes |= PublicationAttributes.AllowPull;
					}

					// Define the pull subscription.
					subscription = new MergePullSubscription();
					subscription.ConnectionContext = subscriberConn;
					subscription.PublisherName = publisherName;
					subscription.PublicationName = publicationName;
					subscription.PublicationDBName = publicationDbName;
					subscription.DatabaseName = subscriptionDbName;
					subscription.HostName = hostname;

					// Specify the Windows login credentials for the Merge Agent job.
					subscription.SynchronizationAgentProcessSecurity.Login = winLogin;
					subscription.SynchronizationAgentProcessSecurity.Password = winPassword;

					// Make sure that the agent job for the subscription is created.
					subscription.CreateSyncAgentByDefault = true;

					// Create the pull subscription at the Subscriber.
					subscription.Create();

					Boolean registered = false;

					// Verify that the subscription is not already registered.
					foreach (MergeSubscription existing
						in publication.EnumSubscriptions())
					{
						if (existing.SubscriberName == subscriberName
							&& existing.SubscriptionDBName == subscriptionDbName
							&& existing.SubscriptionType == SubscriptionOption.Pull)
						{
							registered = true;
						}
					}
					if (!registered)
					{
						// Register the local subscription with the Publisher.
						publication.MakePullSubscriptionWellKnown(
							subscriberName, subscriptionDbName,
							SubscriptionSyncType.Automatic,
							MergeSubscriberType.Local, 0);
					}
				}
				else
				{
					// Do something here if the publication does not exist.
					throw new ApplicationException(String.Format(
						"The publication '{0}' does not exist on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be created.", publicationName), ex);
			}
			finally
			{
				subscriberConn.Disconnect();
				publisherConn.Disconnect();
			}
			//</snippetrmo_CreateMergePullSub>
		}
		static public void CreateMergePushSub(string winLogin, string winPassword)
		{
			//<snippetrmo_CreateMergePushSub>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";
			string hostname = @"adventure-works\garrett1";

			//Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(subscriberName);

			// Create the objects that we need.
			MergePublication publication;
			MergeSubscription subscription;

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Ensure that the publication exists and that 
				// it supports push subscriptions.
				publication = new MergePublication();
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;
				publication.ConnectionContext = conn;

				if (publication.IsExistingObject)
				{
					if ((publication.Attributes & PublicationAttributes.AllowPush) == 0)
					{
						publication.Attributes |= PublicationAttributes.AllowPush;
					}

					// Define the push subscription.
					subscription = new MergeSubscription();
					subscription.ConnectionContext = conn;
					subscription.SubscriberName = subscriberName;
					subscription.PublicationName = publicationName;
					subscription.DatabaseName = publicationDbName;
					subscription.SubscriptionDBName = subscriptionDbName;
					subscription.HostName = hostname;

					// Set a schedule to synchronize the subscription every 2 hours
					// during weekdays from 6am to 10pm.
					subscription.AgentSchedule.FrequencyType = ScheduleFrequencyType.Weekly;
					subscription.AgentSchedule.FrequencyInterval = Convert.ToInt32(0x003E);
					subscription.AgentSchedule.FrequencyRecurrenceFactor = 1;
					subscription.AgentSchedule.FrequencySubDay = ScheduleFrequencySubDay.Hour;
					subscription.AgentSchedule.FrequencySubDayInterval = 2;
					subscription.AgentSchedule.ActiveStartDate = 20051108;
					subscription.AgentSchedule.ActiveEndDate = 20071231;
					subscription.AgentSchedule.ActiveStartTime = 060000;
					subscription.AgentSchedule.ActiveEndTime = 100000;

					// Specify the Windows login credentials for the Merge Agent job.
					subscription.SynchronizationAgentProcessSecurity.Login = winLogin;
					subscription.SynchronizationAgentProcessSecurity.Password = winPassword;

					// Create the push subscription.
					subscription.Create();
				}
				else
				{
					// Do something here if the publication does not exist.
					throw new ApplicationException(String.Format(
						"The publication '{0}' does not exist on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be created.", publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_CreateMergePushSub>
		}
		static public void RemoveMergePullSub()
		{
			//<snippetrmo_DropMergePullSub>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			//Create connections to the Publisher and Subscriber.
			ServerConnection subscriberConn = new ServerConnection(subscriberName);
			ServerConnection publisherConn = new ServerConnection(publisherName);

			// Create the objects that we need.
			MergePublication publication;
			MergePullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				subscriberConn.Connect();

				// Define the pull subscription.
				subscription = new MergePullSubscription();
				subscription.ConnectionContext = subscriberConn;
				subscription.PublisherName = publisherName;
				subscription.PublicationName = publicationName;
				subscription.PublicationDBName = publicationDbName;
				subscription.DatabaseName = subscriptionDbName;

				// Define the publication.
				publication = new MergePublication();
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;
				publication.ConnectionContext = publisherConn;

				// Delete the pull subscription, if it exists.
				if (subscription.IsExistingObject)
				{
					// Delete the pull subscription at the Subscriber.
					subscription.Remove();

					if (publication.LoadProperties())
					{
						publication.RemovePullSubscription(subscriberName, subscriptionDbName);
					}
					else
					{
						// Do something here if the publication does not exist.
						throw new ApplicationException(String.Format(
							"The publication '{0}' does not exist on {1}.",
							publicationName, publisherName));
					}
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The subscription to {0} does not exist on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be deleted.", publicationName), ex);
			}
			finally
			{
				subscriberConn.Disconnect();
				publisherConn.Disconnect();
			}
			//</snippetrmo_DropMergePullSub>
		}
		static public void RemoveTranPullSub()
		{
			//<snippetrmo_DropTranPullSub>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksProductTran";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			//Create connections to the Publisher and Subscriber.
			ServerConnection subscriberConn = new ServerConnection(subscriberName);
			ServerConnection publisherConn = new ServerConnection(publisherName);

			// Create the objects that we need.
			TransPublication publication;
			TransPullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				subscriberConn.Connect();

				// Define the pull subscription.
				subscription = new TransPullSubscription();
				subscription.ConnectionContext = subscriberConn;
				subscription.PublisherName = publisherName;
				subscription.PublicationName = publicationName;
				subscription.PublicationDBName = publicationDbName;
				subscription.DatabaseName = subscriptionDbName;

				// Define the publication.
				publication = new TransPublication();
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;
				publication.ConnectionContext = publisherConn;

				// Delete the pull subscription, if it exists.
				if (subscription.IsExistingObject)
				{
					if (publication.LoadProperties())
					{
                        // Remove the pull subscription registration at the Publisher.
						publication.RemovePullSubscription(subscriberName, subscriptionDbName);
					}
					else
					{
						// Do something here if the publication does not exist.
						throw new ApplicationException(String.Format(
							"The publication '{0}' does not exist on {1}.",
							publicationName, publisherName));
					}
                    // Delete the pull subscription at the Subscriber.
                    subscription.Remove();
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The subscription to {0} does not exist on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be deleted.", publicationName), ex);
			}
			finally
			{
				subscriberConn.Disconnect();
				publisherConn.Disconnect();
			}
			//</snippetrmo_DropTranPullSub>
		}
		static public void RemoveTranPushSub()
		{
			//<snippetrmo_DropTranPushSub>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksProductTran";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			//Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			// Create the objects that we need.
			TransSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				conn.Connect();

				// Define the pull subscription.
				subscription = new TransSubscription();
				subscription.ConnectionContext = conn;
				subscription.SubscriberName = subscriberName;
				subscription.PublicationName = publicationName;
				subscription.SubscriptionDBName = subscriptionDbName;
				subscription.DatabaseName = publicationDbName;

				// Delete the pull subscription, if it exists.
				if (subscription.IsExistingObject)
				{
					// Delete the pull subscription at the Subscriber.
					subscription.Remove();
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The subscription to {0} does not exist on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be deleted.", publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_DropTranPushSub>
		}
		static public void RemoveMergePushSub()
		{
			//<snippetrmo_DropMergePushSub>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			//Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			// Create the objects that we need.
			MergeSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				conn.Connect();

				// Define the pull subscription.
				subscription = new MergeSubscription();
				subscription.ConnectionContext = conn;
				subscription.SubscriberName = subscriberName;
				subscription.PublicationName = publicationName;
				subscription.SubscriptionDBName = subscriptionDbName;
				subscription.DatabaseName = publicationDbName;

				// Delete the pull subscription, if it exists.
				if (subscription.IsExistingObject)
				{
					// Delete the pull subscription at the Subscriber.
					subscription.Remove();
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The subscription to {0} does not exist on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be deleted.", publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_DropMergePushSub>
		}
		static public void SyncTranPullSub()
		{
			//<snippetrmo_SyncTranPullSub>
            // Define the server, publication, and database names.
            string subscriberName = subscriberInstance;
            string publisherName = publisherInstance;
            string publicationName = "AdvWorksProductTran";
            string subscriptionDbName = "AdventureWorks2012Replica";
            string publicationDbName = "AdventureWorks2012";

            // Create a connection to the Subscriber.
            ServerConnection conn = new ServerConnection(subscriberName);

            TransPullSubscription subscription;

            try
            {
	            // Connect to the Subscriber.
	            conn.Connect();

	            // Define the pull subscription.
	            subscription = new TransPullSubscription();
	            subscription.ConnectionContext = conn;
	            subscription.DatabaseName = subscriptionDbName;
	            subscription.PublisherName = publisherName;
	            subscription.PublicationDBName = publicationDbName;
	            subscription.PublicationName = publicationName;

	            // If the pull subscription exists, then start the synchronization.
	            if (subscription.LoadProperties())
	            {
		            // Check that we have enough metadata to start the agent.
		            if (subscription.PublisherSecurity != null)
		            {
                        // Synchronously start the Distribution Agent for the subscription.
			            subscription.SynchronizationAgent.Synchronize();
		            }
		            else
		            {
			            throw new ApplicationException("There is insufficent metadata to " +
				            "synchronize the subscription. Recreate the subscription with " +
				            "the agent job or supply the required agent properties at run time.");
		            }
	            }
	            else
	            {
		            // Do something here if the pull subscription does not exist.
		            throw new ApplicationException(String.Format(
			            "A subscription to '{0}' does not exist on {1}",
			            publicationName, subscriberName));
	            }
            }
            catch (Exception ex)
            {
	            // Implement appropriate error handling here.
	            throw new ApplicationException("The subscription could not be " +
		            "synchronized. Verify that the subscription has " +
		            "been defined correctly.", ex);
            }
            finally
            {
	            conn.Disconnect();
            }
			//</snippetrmo_SyncTranPullSub>
		}
		static public void SyncTranPushSub()
		{
			//<snippetrmo_SyncTranPushSub>
			// Define the server, publication, and database names.
			string subscriberName = subscriberInstance;
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksProductTran";
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			TransSubscription subscription;

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Define the push subscription.
				subscription = new TransSubscription();
				subscription.ConnectionContext = conn;
				subscription.DatabaseName = publicationDbName;
				subscription.PublicationName = publicationName;
				subscription.SubscriptionDBName = subscriptionDbName;
				subscription.SubscriberName = subscriberName;

				// If the push subscription exists, start the synchronization.
				if (subscription.LoadProperties())
				{
					// Check that we have enough metadata to start the agent.
					if (subscription.SubscriberSecurity != null)
					{
						// Synchronously start the Distribution Agent for the subscription.
						subscription.SynchronizationAgent.Synchronize();
					}
					else
					{
						throw new ApplicationException("There is insufficent metadata to " +
							"synchronize the subscription. Recreate the subscription with " +
							"the agent job or supply the required agent properties at run time.");
					}
				}
				else
				{
					// Do something here if the push subscription does not exist.
					throw new ApplicationException(String.Format(
						"The subscription to '{0}' does not exist on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("The subscription could not be synchronized.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_SyncTranPushSub>
		}
		static public void SyncTranPullSubWithJob()
		{
			//<snippetrmo_SyncTranPullSub_WithJob>
			// Define server, publication, and database names.
			String subscriberName = subscriberInstance;
			String publisherName = publisherInstance;
			String publicationName = "AdvWorksProductTran";
			String publicationDbName = "AdventureWorks2012";
            String subscriptionDbName = "AdventureWorks2012Replica";

			// Create a connection to the Subscriber.
			ServerConnection conn = new ServerConnection(subscriberName);

			TransPullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				conn.Connect();

				// Define subscription properties.
				subscription = new TransPullSubscription();
				subscription.ConnectionContext = conn;
                subscription.DatabaseName = subscriptionDbName;
				subscription.PublisherName = publisherName;
				subscription.PublicationDBName = publicationDbName;
				subscription.PublicationName = publicationName;

				// If the pull subscription and the job exists, start the agent job.
				if (subscription.LoadProperties() && subscription.AgentJobId != null)
				{
					subscription.SynchronizeWithJob();
				}
				else
				{
					// Do something here if the subscription does not exist.
					throw new ApplicationException(String.Format(
						"A subscription to '{0}' does not exists on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Do appropriate error handling here.
				throw new ApplicationException("The subscription could not be synchronized.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_SyncTranPullSub_WithJob>
		}
		static public void SyncTranPushSubWithJob()
		{
			//<snippetrmo_SyncTranPushSub_WithJob>
			// Define the server, publication, and database names.
			string subscriberName = subscriberInstance;
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksProductTran";
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			/// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			TransSubscription subscription;

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Instantiate the push subscription.
				subscription = new TransSubscription();
				subscription.ConnectionContext = conn;
				subscription.DatabaseName = publicationDbName;
				subscription.PublicationName = publicationName;
				subscription.SubscriptionDBName = subscriptionDbName;
				subscription.SubscriberName = subscriberName;

				// If the push subscription and the job exists, start the agent job.
				if (subscription.LoadProperties() && subscription.AgentJobId != null)
				{
					// Start the Distribution Agent asynchronously.
					subscription.SynchronizeWithJob();
				}
				else
				{
					// Do something here if the subscription does not exist.
					throw new ApplicationException(String.Format(
						"A subscription to '{0}' does not exists on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("The subscription could not be synchronized.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_SyncTranPushSub_WithJob>
		}
		static public void SyncMergePullSubWithJob()
		{
			//<snippetrmo_SyncMergePullSub_WithJob>
			// Define server, publication, and database names.
			String subscriberName = subscriberInstance;
			String publisherName = publisherInstance;
			String publicationName = "AdvWorksSalesOrdersMerge";
			String publicationDbName = "AdventureWorks2012";
            String subscriptionDbName = "AdventureWorks2012Replica";

			// Create a connection to the Subscriber.
			ServerConnection conn = new ServerConnection(subscriberName);

			MergePullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				conn.Connect();

				// Define subscription properties.
				subscription = new MergePullSubscription();
				subscription.ConnectionContext = conn;
				subscription.DatabaseName = subscriptionDbName;
				subscription.PublisherName = publisherName;
				subscription.PublicationDBName = publicationDbName;
				subscription.PublicationName = publicationName;

				// If the pull subscription and the job exists, start the agent job.
				if (subscription.LoadProperties() && subscription.AgentJobId != null)
				{
					subscription.SynchronizeWithJob();
				}
				else
				{
					// Do something here if the subscription does not exist.
					throw new ApplicationException(String.Format(
						"A subscription to '{0}' does not exists on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Do appropriate error handling here.
				throw new ApplicationException("The subscription could not be synchronized.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_SyncMergePullSub_WithJob>
		}
		static public void SyncMergePullSub()
		{
			//<snippetrmo_SyncMergePullSub>
			// Define the server, publication, and database names.
			string subscriberName = subscriberInstance;
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			// Create a connection to the Subscriber.
			ServerConnection conn = new ServerConnection(subscriberName);

			MergePullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				conn.Connect();

				// Define the pull subscription.
				subscription = new MergePullSubscription();
				subscription.ConnectionContext = conn;
				subscription.DatabaseName = subscriptionDbName;
				subscription.PublisherName = publisherName;
				subscription.PublicationDBName = publicationDbName;
				subscription.PublicationName = publicationName;

				// If the pull subscription exists, then start the synchronization.
				if (subscription.LoadProperties())
				{
					// Check that we have enough metadata to start the agent.
					if (subscription.PublisherSecurity != null || subscription.DistributorSecurity != null)
					{
						// Synchronously start the Merge Agent for the subscription.
						subscription.SynchronizationAgent.Synchronize();
					}
					else
					{
						throw new ApplicationException("There is insufficent metadata to " +
							"synchronize the subscription. Recreate the subscription with " +
							"the agent job or supply the required agent properties at run time.");
					}
				}
				else
				{
					// Do something here if the pull subscription does not exist.
					throw new ApplicationException(String.Format(
						"A subscription to '{0}' does not exist on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("The subscription could not be " +
					"synchronized. Verify that the subscription has " +
					"been defined correctly.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_SyncMergePullSub>
		}
		static public void SyncMergePushSub()
		{
			//<snippetrmo_SyncMergePushSub>
			// Define the server, publication, and database names.
			string subscriberName = subscriberInstance;
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			MergeSubscription subscription;

			try
			{
				// Connect to the Publisher
				conn.Connect();

				// Define the subscription.
				subscription = new MergeSubscription();
				subscription.ConnectionContext = conn;
				subscription.DatabaseName = publicationDbName;
				subscription.PublicationName = publicationName;
				subscription.SubscriptionDBName = subscriptionDbName;
				subscription.SubscriberName = subscriberName;

				// If the push subscription exists, start the synchronization.
				if (subscription.LoadProperties())
				{
					// Check that we have enough metadata to start the agent.
					if (subscription.SubscriberSecurity != null)
					{
						// Synchronously start the Merge Agent for the subscription.
						subscription.SynchronizationAgent.Synchronize();
					}
					else
					{
						throw new ApplicationException("There is insufficent metadata to " +
							"synchronize the subscription. Recreate the subscription with " +
							"the agent job or supply the required agent properties at run time.");
					}
				}
				else
				{
					// Do something here if the push subscription does not exist.
					throw new ApplicationException(String.Format(
						"The subscription to '{0}' does not exist on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("The subscription could not be synchronized.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_SyncMergePushSub>
		}
		static public void SyncMergePushSubWithJob()
		{
			//<snippetrmo_SyncMergePushSub_WithJob>
			// Define the server, publication, and database names.
			string subscriberName = subscriberInstance;
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			MergeSubscription subscription;

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Define push subscription.
				subscription = new MergeSubscription();
				subscription.ConnectionContext = conn;
				subscription.DatabaseName = publicationDbName;
				subscription.PublicationName = publicationName;
				subscription.SubscriptionDBName = subscriptionDbName;
				subscription.SubscriberName = subscriberName;

				// If the push subscription and the job exists, start the agent job.
				if (subscription.LoadProperties() && subscription.AgentJobId != null)
				{
					// Start the Merge Agent asynchronously.
					subscription.SynchronizeWithJob();
				}
				else
				{
					// Do something here if the subscription does not exist.
					throw new ApplicationException(String.Format(
						"A subscription to '{0}' does not exists on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("The subscription could not be synchronized.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_SyncMergePushSub_WithJob>
		}
		static public void ConfigureWebSync(string winLogin, string winPassword)
		{
			//<snippetrmo_CreateMergePub_WebSync>
			// Set the Publisher, publication database, and publication names.
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publicationDbName = "AdventureWorks2012";

			ReplicationDatabase publicationDb;
			MergePublication publication;

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Enable the database for merge publication.				
				publicationDb = new ReplicationDatabase(publicationDbName, conn);
				if (publicationDb.LoadProperties())
				{
					if (!publicationDb.EnabledMergePublishing)
					{
						publicationDb.EnabledMergePublishing = true;
					}
				}
				else
				{
					// Do something here if the database does not exist. 
					throw new ApplicationException(String.Format(
						"The {0} database does not exist on {1}.",
						publicationDb, publisherName));
				}

				// Set the required properties for the merge publication.
				publication = new MergePublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;

				// Enable Web synchronization, if not already enabled.
				if ((publication.Attributes & PublicationAttributes.AllowWebSynchronization) == 0)
				{
					publication.Attributes |= PublicationAttributes.AllowWebSynchronization;
				}

				// Enable pull subscriptions, if not already enabled.
				if ((publication.Attributes & PublicationAttributes.AllowPull) == 0)
				{
					publication.Attributes |= PublicationAttributes.AllowPull;
				}
				
				// Enable Subscriber requested snapshot generation. 
				publication.Attributes |= PublicationAttributes.AllowSubscriberInitiatedSnapshot;

                // Enable anonymous access for Subscribers that cannot make a direct connetion 
                // to the Publisher. 
                publication.Attributes |= PublicationAttributes.AllowAnonymous;

				// Specify the Windows account under which the Snapshot Agent job runs.
				// This account will be used for the local connection to the 
				// Distributor and all agent connections that use Windows Authentication.
				publication.SnapshotGenerationAgentProcessSecurity.Login = winLogin;
				publication.SnapshotGenerationAgentProcessSecurity.Password = winPassword;

				// Explicitly set the security mode for the Publisher connection
				// Windows Authentication (the default).
				publication.SnapshotGenerationAgentPublisherSecurity.WindowsAuthentication = true;

				if (!publication.IsExistingObject)
				{
					// Create the merge publication and the Snapshot Agent job.
					publication.Create();
					publication.CreateSnapshotAgent();
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The {0} publication already exists.", publicationName));
				}
			}

			catch (Exception ex)
			{
				// Implement custom application error handling here.
				throw new ApplicationException(String.Format(
					"The publication {0} could not be created.", publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_CreateMergePub_WebSync>
		}
		static public void CreateMergePullSubWebSync(string winLogin, string winPassword)
		{
			//<snippetrmo_CreateMergePullSub_WebSync>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";
			string hostname = @"adventure-works\garrett1";
			string webSyncUrl = "https://" + publisherInstance + "/WebSync/replisapi.dll";

			//Create connections to the Publisher and Subscriber.
			ServerConnection subscriberConn = new ServerConnection(subscriberName);
			ServerConnection publisherConn = new ServerConnection(publisherName);

			// Create the objects that we need.
			MergePublication publication;
			MergePullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				subscriberConn.Connect();

				// Ensure that the publication exists and that 
				// it supports pull subscriptions and Web synchronization.
				publication = new MergePublication();
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;
				publication.ConnectionContext = publisherConn;

				if (publication.LoadProperties())
				{
					if ((publication.Attributes & PublicationAttributes.AllowPull) == 0)
					{
						publication.Attributes |= PublicationAttributes.AllowPull;
					}
					if ((publication.Attributes & PublicationAttributes.AllowWebSynchronization) == 0)
					{
						publication.Attributes |= PublicationAttributes.AllowWebSynchronization;
					}

					// Define the pull subscription.
					subscription = new MergePullSubscription();
					subscription.ConnectionContext = subscriberConn;
					subscription.PublisherName = publisherName;
					subscription.PublicationName = publicationName;
					subscription.PublicationDBName = publicationDbName;
					subscription.DatabaseName = subscriptionDbName;
					subscription.HostName = hostname;

					// Specify the Windows login credentials for the Merge Agent job.
					subscription.SynchronizationAgentProcessSecurity.Login = winLogin;
					subscription.SynchronizationAgentProcessSecurity.Password = winPassword;

					// Enable Web synchronization.
					subscription.UseWebSynchronization = true;
					subscription.InternetUrl = webSyncUrl;

					// Specify the same Windows credentials to use when connecting to the
					// Web server using HTTPS Basic Authentication.
					subscription.InternetSecurityMode = AuthenticationMethod.BasicAuthentication;
					subscription.InternetLogin = winLogin;
					subscription.InternetPassword = winPassword;

                    // Ensure that we create a job for this subscription.
                    subscription.CreateSyncAgentByDefault = true;

					// Create the pull subscription at the Subscriber.
					subscription.Create();

					Boolean registered = false;

					// Verify that the subscription is not already registered.
					foreach (MergeSubscription existing
						in publication.EnumSubscriptions())
					{
						if (existing.SubscriberName == subscriberName
							&& existing.SubscriptionDBName == subscriptionDbName
							&& existing.SubscriptionType == SubscriptionOption.Pull)
						{
							registered = true;
						}
					}
					if (!registered)
					{
						// Register the local subscription with the Publisher.
						publication.MakePullSubscriptionWellKnown(
							subscriberName, subscriptionDbName,
							SubscriptionSyncType.Automatic,
							MergeSubscriberType.Local, 0);
					}
				}
				else
				{
					// Do something here if the publication does not exist.
					throw new ApplicationException(String.Format(
						"The publication '{0}' does not exist on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be created.", publicationName), ex);
			}
			finally
			{
				subscriberConn.Disconnect();
				publisherConn.Disconnect();
			}
			//</snippetrmo_CreateMergePullSub_WebSync>
		}
		static public void CreateMergePullSubNoJob()
		{
			//<snippetrmo_CreateMergePullSub_NoJob>
			// Define the Publisher, publication, and databases.
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publisherName = publisherInstance;
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";

			//Create connections to the Publisher and Subscriber.
			ServerConnection subscriberConn = new ServerConnection(subscriberName);
			ServerConnection publisherConn = new ServerConnection(publisherName);

			// Create the objects that we need.
			MergePublication publication;
			MergePullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				subscriberConn.Connect();

				// Ensure that the publication exists and that 
				// it supports pull subscriptions.
				publication = new MergePublication();
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;
				publication.ConnectionContext = publisherConn;

				if (publication.LoadProperties())
				{
					if ((publication.Attributes & PublicationAttributes.AllowPull) == 0)
					{
						publication.Attributes |= PublicationAttributes.AllowPull;
					}

					// Define the pull subscription.
					subscription = new MergePullSubscription();
					subscription.ConnectionContext = subscriberConn;
					subscription.PublisherName = publisherName;
					subscription.PublicationName = publicationName;
					subscription.PublicationDBName = publicationDbName;
					subscription.DatabaseName = subscriptionDbName;

					// Specify that an agent job not be created for this subscription. The
					// subscription can only be synchronized by running the Merge Agent directly.
					// Subscripition metadata stored in MSsubscription_properties will not
					// be available and must be specified at run time.
					subscription.CreateSyncAgentByDefault = false;

					// Create the pull subscription at the Subscriber.
					subscription.Create();

					Boolean registered = false;

					// Verify that the subscription is not already registered.
					foreach (MergeSubscription existing
						in publication.EnumSubscriptions())
					{
						if (existing.SubscriberName == subscriberName
							&& existing.SubscriptionDBName == subscriptionDbName
							&& existing.SubscriptionType == SubscriptionOption.Pull)
						{
							registered = true;
						}
					}
					if (!registered)
					{
						// Register the local subscription with the Publisher.
						publication.MakePullSubscriptionWellKnown(
							subscriberName, subscriptionDbName,
							SubscriptionSyncType.Automatic,
							MergeSubscriberType.Local, 0);
					}
				}
				else
				{
					// Do something here if the publication does not exist.
					throw new ApplicationException(String.Format(
						"The publication '{0}' does not exist on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here.
				throw new ApplicationException(String.Format(
					"The subscription to {0} could not be created.", publicationName), ex);
			}
			finally
			{
				subscriberConn.Disconnect();
				publisherConn.Disconnect();
			}
			//</snippetrmo_CreateMergePullSub_NoJob>
		}
		static public void SyncMergePullSubNoJobWebSync(string winLogin, string winPassword)
		{
			//<snippetrmo_SyncMergePullSub_NoJobWebSync>
			// Define the server, publication, and database names.
			string subscriberName = subscriberInstance;
			string publisherName = publisherInstance;
            string distributorName = distributorInstance;
            string publicationName = "AdvWorksSalesOrdersMerge";
			string subscriptionDbName = "AdventureWorks2012Replica";
			string publicationDbName = "AdventureWorks2012";
			string hostname = @"adventure-works\garrett1";
			string webSyncUrl = "https://" + publisherInstance + "/SalesOrders/replisapi.dll";

			// Create a connection to the Subscriber.
			ServerConnection conn = new ServerConnection(subscriberName);

			MergePullSubscription subscription;
			MergeSynchronizationAgent agent;

			try
			{
				// Connect to the Subscriber.
				conn.Connect();

				// Define the pull subscription.
				subscription = new MergePullSubscription();
				subscription.ConnectionContext = conn;
				subscription.DatabaseName = subscriptionDbName;
				subscription.PublisherName = publisherName;
				subscription.PublicationDBName = publicationDbName;
				subscription.PublicationName = publicationName;

				// If the pull subscription exists, then start the synchronization.
				if (subscription.LoadProperties())
				{
					// Get the agent for the subscription.
					agent = subscription.SynchronizationAgent;

					// Check that we have enough metadata to start the agent.
					if (agent.PublisherSecurityMode == null)
					{
						// Set the required properties that could not be returned
						// from the MSsubscription_properties table. 
						agent.PublisherSecurityMode = SecurityMode.Integrated;
						agent.DistributorSecurityMode = SecurityMode.Integrated;
                        agent.Distributor = publisherName;
                        agent.HostName = hostname;

						// Set optional Web synchronization properties.
						agent.UseWebSynchronization = true;
						agent.InternetUrl = webSyncUrl;
						agent.InternetSecurityMode = SecurityMode.Standard;
						agent.InternetLogin = winLogin;
						agent.InternetPassword = winPassword;
					}
					// Enable agent output to the console.
					agent.OutputVerboseLevel = 1;
					agent.Output = "";

					// Synchronously start the Merge Agent for the subscription.
					agent.Synchronize();
				}
				else
				{
					// Do something here if the pull subscription does not exist.
					throw new ApplicationException(String.Format(
						"A subscription to '{0}' does not exist on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Implement appropriate error handling here.
				throw new ApplicationException("The subscription could not be " +
					"synchronized. Verify that the subscription has " +
					"been defined correctly.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_SyncMergePullSub_NoJobWebSync>
		}
		static public void GeneratedSnapshotWithJob()
		{
			//<snippetrmo_GenerateSnapshot_WithJob>
			// Set the Publisher, publication database, and publication names.
			string publicationName = "AdvWorksProductTran";
			string publicationDbName = "AdventureWorks2012";
			string publisherName = publisherInstance;

			TransPublication publication;

			// Create a connection to the Publisher using Windows Authentication.
			ServerConnection conn;
			conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Set the required properties for an existing publication.
				publication = new TransPublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;

				if (publication.LoadProperties())
				{
					// Start the Snapshot Agent job for the publication.
					publication.StartSnapshotGenerationAgentJob();
				}
				else
				{
					throw new ApplicationException(String.Format(
						"The {0} publication does not exist.", publicationName));
				}
			}
			catch (Exception ex)
			{
				// Implement custom application error handling here.
				throw new ApplicationException(String.Format(
					"A snapshot could not be generated for the {0} publication."
					, publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_GenerateSnapshot_WithJob>
		}
		static public void GenerateTranSnapshot()
		{
			//<snippetrmo_GenerateSnapshot>
			// Set the Publisher, publication database, and publication names.
			string publicationName = "AdvWorksProductTran";
			string publicationDbName = "AdventureWorks2012";
			string publisherName = publisherInstance;
			string distributorName = publisherInstance;

			SnapshotGenerationAgent agent;

			try
			{
				// Set the required properties for Snapshot Agent.
				agent = new SnapshotGenerationAgent();
				agent.Distributor = distributorName;
				agent.DistributorSecurityMode = SecurityMode.Integrated;
				agent.Publisher = publisherName;
				agent.PublisherSecurityMode = SecurityMode.Integrated;
				agent.Publication = publicationName;
				agent.PublisherDatabase = publicationDbName;
				agent.ReplicationType = ReplicationType.Transactional;

				// Start the agent synchronously.
				agent.GenerateSnapshot();

			}
			catch (Exception ex)
			{
				// Implement custom application error handling here.
				throw new ApplicationException(String.Format(
					"A snapshot could not be generated for the {0} publication."
					, publicationName), ex);
			}
			//</snippetrmo_GenerateSnapshot>
		}
        static public void GenerateMergeSnapshot()
        {
            //<snippetrmo_GenerateMergeSnapshot>
            // Set the Publisher, publication database, and publication names.
            string publicationName = "AdvWorksSalesOrdersMerge";
            string publicationDbName = "AdventureWorks2012";
            string publisherName = publisherInstance;
            string distributorName = publisherInstance;

            SnapshotGenerationAgent agent;

            try
            {
                // Set the required properties for Snapshot Agent.
                agent = new SnapshotGenerationAgent();
                agent.Distributor = distributorName;
                agent.DistributorSecurityMode = SecurityMode.Integrated;
                agent.Publisher = publisherName;
                agent.PublisherSecurityMode = SecurityMode.Integrated;
                agent.Publication = publicationName;
                agent.PublisherDatabase = publicationDbName;
                agent.ReplicationType = ReplicationType.Merge;

                // Start the agent synchronously.
                agent.GenerateSnapshot();

            }
            catch (Exception ex)
            {
                // Implement custom application error handling here.
                throw new ApplicationException(String.Format(
                    "A snapshot could not be generated for the {0} publication."
                    , publicationName), ex);
            }
            //</snippetrmo_GenerateMergeSnapshot>
        }
        static public void GeneratedFilteredSnapshot(string hostname)
		{
			//<snippetrmo_GenerateFilteredSnapshot>
			// Set the Publisher, publication database, and publication names.
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publicationDbName = "AdventureWorks2012";
			string publisherName = publisherInstance;
			string distributorName = publisherInstance;

			SnapshotGenerationAgent agent;

			try
			{
				// Set the required properties for Snapshot Agent.
				agent = new SnapshotGenerationAgent();
				agent.Distributor = distributorName;
				agent.DistributorSecurityMode = SecurityMode.Integrated;
				agent.Publisher = publisherName;
				agent.PublisherSecurityMode = SecurityMode.Integrated;
				agent.Publication = publicationName;
				agent.PublisherDatabase = publicationDbName;
				agent.ReplicationType = ReplicationType.Merge;

				// Specify the partition information to generate a 
				// filtered snapshot based on Hostname.
				agent.DynamicFilterHostName = hostname;

				// Start the agent synchronously.
				agent.GenerateSnapshot();
			}
			catch (Exception ex)
			{
				// Implement custom application error handling here.
				throw new ApplicationException(String.Format(
					"A snapshot could not be generated for the {0} publication."
					, publicationName), ex);
			}
			//</snippetrmo_GenerateFilteredSnapshot>
		}
		static public void CreateMergePartition(string hostname)
		{
			//<snippetrmo_CreateMergePartition>
			// Define the server, database, and publication names
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publicationDbName = "AdventureWorks2012";
			string distributorName = publisherInstance;

			MergePublication publication;
			MergePartition partition;
			MergeDynamicSnapshotJob snapshotAgentJob;
			ReplicationAgentSchedule schedule;
			
			// Create a connection to the Publisher.
			ServerConnection publisherConn = new ServerConnection(publisherName);

			// Create a connection to the Distributor to start the Snapshot Agent.
			ServerConnection distributorConn = new ServerConnection(distributorName);

			try
			{
				// Connect to the Publisher.
				publisherConn.Connect();

				// Set the required properties for the publication.
				publication = new MergePublication();
				publication.ConnectionContext = publisherConn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;


				// If we can't get the properties for this merge publication, 
				// then throw an application exception.
				if (publication.LoadProperties() || publication.SnapshotAvailable)
				{
					// Set a weekly schedule for the filtered data snapshot.
					schedule = new ReplicationAgentSchedule();
					schedule.FrequencyType = ScheduleFrequencyType.Weekly;
					schedule.FrequencyRecurrenceFactor = 1;
					schedule.FrequencyInterval = Convert.ToInt32(0x001);

					// Set the value of Hostname that defines the data partition. 
					partition = new MergePartition();
					partition.DynamicFilterHostName = hostname;
					snapshotAgentJob = new MergeDynamicSnapshotJob();
					snapshotAgentJob.DynamicFilterHostName = hostname;

					// Create the partition for the publication with the defined schedule.
					publication.AddMergePartition(partition);
					publication.AddMergeDynamicSnapshotJob(snapshotAgentJob, schedule);
				}
				else
				{
					throw new ApplicationException(String.Format(
						"Settings could not be retrieved for the publication, " +
						" or the initial snapshot has not been generated. " +
						"Ensure that the publication {0} exists on {1} and " +
						"that the Snapshot Agent has run successfully.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Do error handling here.
				throw new ApplicationException(string.Format(
					"The partition for '{0}' in the {1} publication could not be created.",
					hostname, publicationName), ex);
			}
			finally
			{
				publisherConn.Disconnect();
				if (distributorConn.IsOpen) distributorConn.Disconnect();
			}
			//</snippetrmo_CreateMergePartition>
		}
		static public void RegisterBLH()
		{
			//<snippetrmo_RegisterBLH>
			// Specify the Distributor name and business logic properties.
			string distributorName = publisherInstance;
			string assemblyName = @"C:\Program Files\Microsoft SQL Server\90\COM\CustomLogic.dll";
			string className = "Microsoft.Samples.SqlServer.BusinessLogicHandler.OrderEntryBusinessLogicHandler";
			string friendlyName = "OrderEntryLogic";

			ReplicationServer distributor;
			BusinessLogicHandler customLogic;

				// Create a connection to the Distributor.
			ServerConnection distributorConn = new ServerConnection(distributorName);

			try
			{
				// Connect to the Distributor.
				distributorConn.Connect();

				// Set the Distributor properties.
				distributor = new ReplicationServer(distributorConn);

				// Set the business logic handler properties.
				customLogic = new BusinessLogicHandler();
				customLogic.DotNetAssemblyName = assemblyName;
				customLogic.DotNetClassName = className;
				customLogic.FriendlyName = friendlyName;
				customLogic.IsDotNetAssembly = true;

				Boolean isRegistered = false;

				// Check if the business logic handler is already registered at the Distributor.
				foreach (BusinessLogicHandler registeredLogic
					in distributor.EnumBusinessLogicHandlers())
				{
					if (registeredLogic == customLogic)
					{
						isRegistered = true;
					}
				}

				// Register the custom logic.
				if (!isRegistered)
				{
					distributor.RegisterBusinessLogicHandler(customLogic);
				}
			}
			catch (Exception ex)
			{
				// Do error handling here.
				throw new ApplicationException(string.Format(
					"The {0} assembly could not be registered.",
					assemblyName), ex);
			}
			finally
			{
				distributorConn.Disconnect();
			}
			//</snippetrmo_RegisterBLH>
		}
		static public void RegisterBLH_10()
		{
			//<snippetrmo_RegisterBLH_10>
			// Specify the Distributor name and business logic properties.
			string distributorName = publisherInstance;
			string assemblyName = @"C:\Program Files\Microsoft SQL Server\110\COM\CustomLogic.dll";
			string className = "Microsoft.Samples.SqlServer.BusinessLogicHandler.OrderEntryBusinessLogicHandler";
			string friendlyName = "OrderEntryLogic";

			ReplicationServer distributor;
			BusinessLogicHandler customLogic;

				// Create a connection to the Distributor.
			ServerConnection distributorConn = new ServerConnection(distributorName);

			try
			{
				// Connect to the Distributor.
				distributorConn.Connect();

				// Set the Distributor properties.
				distributor = new ReplicationServer(distributorConn);

				// Set the business logic handler properties.
				customLogic = new BusinessLogicHandler();
				customLogic.DotNetAssemblyName = assemblyName;
				customLogic.DotNetClassName = className;
				customLogic.FriendlyName = friendlyName;
				customLogic.IsDotNetAssembly = true;

				Boolean isRegistered = false;

				// Check if the business logic handler is already registered at the Distributor.
				foreach (BusinessLogicHandler registeredLogic
					in distributor.EnumBusinessLogicHandlers())
				{
					if (registeredLogic == customLogic)
					{
						isRegistered = true;
					}
				}

				// Register the custom logic.
				if (!isRegistered)
				{
					distributor.RegisterBusinessLogicHandler(customLogic);
				}
			}
			catch (Exception ex)
			{
				// Do error handling here.
				throw new ApplicationException(string.Format(
					"The {0} assembly could not be registered.",
					assemblyName), ex);
			}
			finally
			{
				distributorConn.Disconnect();
			}
			//</snippetrmo_RegisterBLH_10>
		}
		static public void ChangeMergeArticle_BLH()
		{
			//<snippetrmo_ChangeMergeArticle_BLH>
			// Define the Publisher, publication, and article names.
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publicationDbName = "AdventureWorks2012";
			string articleName = "SalesOrderHeader";
			
			// Set the friendly name of the business logic handler.
			string customLogic = "OrderEntryLogic";

			MergeArticle article = new MergeArticle();
			
			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Set the required properties for the article.
				article.ConnectionContext = conn;
				article.Name = articleName;
				article.DatabaseName = publicationDbName;
				article.PublicationName = publicationName;

				// Load the article properties.
				if (article.LoadProperties())
				{
					article.ArticleResolver = customLogic;
				}
				else
				{
					// Throw an exception of the article does not exist.
					throw new ApplicationException(String.Format(
					"{0} is not published in {1}", articleName, publicationName));
				}
				
			}
			catch (Exception ex)
			{
				// Do error handling here and rollback the transaction.
				throw new ApplicationException(String.Format(
					"The business logic handler {0} could not be associated with " +
					" the {1} article.",customLogic,articleName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_ChangeMergeArticle_BLH>
		}
		static public void ReinitTranPullSub()
		{
			//<snippetrmo_ReinitTranPullSub>
			// Define server, publication, and database names.
			String subscriberName = subscriberInstance;
			String publisherName = publisherInstance;
			String publicationName = "AdvWorksProductTran";
			String publicationDbName = "AdventureWorks2012";
            String subscriptionDbName = "AdventureWorks2012Replica";

			// Create a connection to the Subscriber.
			ServerConnection conn = new ServerConnection(subscriberName);

			TransPullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				conn.Connect();

				// Define subscription properties.
				subscription = new TransPullSubscription();
				subscription.ConnectionContext = conn;
                subscription.DatabaseName = subscriptionDbName;
				subscription.PublisherName = publisherName;
				subscription.PublicationDBName = publicationDbName;
				subscription.PublicationName = publicationName;

				// If the pull subscription and the job exists, mark the subscription
				// for reinitialization and start the agent job.
				if (subscription.LoadProperties() && subscription.AgentJobId != null)
				{
					subscription.Reinitialize();
					subscription.SynchronizeWithJob();
				}
				else
				{
					// Do something here if the subscription does not exist.
					throw new ApplicationException(String.Format(
						"A subscription to '{0}' does not exists on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Do appropriate error handling here.
				throw new ApplicationException("The subscription could not be reinitialized.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_ReinitTranPullSub>
		}
		static public void ReinitMergePullSub_WithUpload()
		{
			//<snippetrmo_ReinitMergePullSub_WithUpload>
			// Define server, publication, and database names.
			String subscriberName = subscriberInstance;
			String publisherName = publisherInstance;
			String publicationName = "AdvWorksSalesOrdersMerge";
			String publicationDbName = "AdventureWorks2012";
            String subscriptionDbName = "AdventureWorks2012Replica";

			// Create a connection to the Subscriber.
			ServerConnection conn = new ServerConnection(subscriberName);

			MergePullSubscription subscription;

			try
			{
				// Connect to the Subscriber.
				conn.Connect();

				// Define subscription properties.
				subscription = new MergePullSubscription();
				subscription.ConnectionContext = conn;
                subscription.DatabaseName = subscriptionDbName;
				subscription.PublisherName = publisherName;
				subscription.PublicationDBName = publicationDbName;
				subscription.PublicationName = publicationName;

				// If the pull subscription and the job exists, mark the subscription
				// for reinitialization after upload and start the agent job.
				if (subscription.LoadProperties() && subscription.AgentJobId != null)
				{
					subscription.Reinitialize(true);
					subscription.SynchronizeWithJob();
				}
				else
				{
					// Do something here if the subscription does not exist.
					throw new ApplicationException(String.Format(
						"A subscription to '{0}' does not exists on {1}",
						publicationName, subscriberName));
				}
			}
			catch (Exception ex)
			{
				// Do appropriate error handling here.
				throw new ApplicationException("The subscription could not be synchronized.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_ReinitMergePullSub_WithUpload>
		}
		static public void OutputParams()
		{

		}
		static public void ChangeServerPasswords(string login, string password)
		{
			//<snippetrmo_ChangeServerPasswords>
			// Set the Distributor and distribution database names.
			string serverName = publisherInstance;

			ReplicationServer server;
			
			// Create a connection to the Distributor using Windows Authentication.
			ServerConnection conn = new ServerConnection(serverName);

			try
			{
				// Open the connection. 
				conn.Connect();

				server = new ReplicationServer(conn);

				// Load server properties, if it exists.
				if (server.LoadProperties())
				{
					string[] slash = new string[1];
					slash[1] = @"\";

					// If the login is in the form string\string, assume we are 
					// changing the password for a Windows login.
					if (login.Split(slash, StringSplitOptions.None).Length == 2)
					{
						//Change the password for the all connections that use
						// the Windows login. 
						server.ChangeReplicationServerPasswords(
								ReplicationSecurityMode.Integrated, login, password);
					}
					else
					{
						// Change the password for the all connections that use
						// the SQL Server login. 
						server.ChangeReplicationServerPasswords(
								ReplicationSecurityMode.SqlStandard, login, password);
					}
				}
				else
				{
					throw new ApplicationException(String.Format(
						"Properties for {0} could not be retrieved.", publisherInstance));
				}
			}
			catch (Exception ex)
			{
				// Implement the appropriate error handling here. 
				throw new ApplicationException(String.Format(
					"An error occured when changing agent login " +
					" credentials on {0}.",serverName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_ChangeServerPasswords>

		}
		static public void ValidateTranPublication()
		{
			//<snippetrmo_ValidateTranPub>
			// Define the server, database, and publication names
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksProductTran";
			string publicationDbName = "AdventureWorks2012";

			TransPublication publication;

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Set the required properties for the publication.
				publication = new TransPublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;

				// If we can't get the properties for this publication, 
				// throw an application exception.
				if (publication.LoadProperties())
				{
					// Initiate validataion for all subscriptions to this publication.
					publication.ValidatePublication(ValidationOption.RowCountOnly,
						ValidationMethod.ConditionalFast, false);

					// If not already running, start the Distribution Agent at each 
					// Subscriber to synchronize and validate the subscriptions.
				}
				else
				{
					throw new ApplicationException(String.Format(
						"Settings could not be retrieved for the publication. " +
						"Ensure that the publication {0} exists on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Do error handling here.
				throw new ApplicationException(
					"Subscription validation could not be initiated.", ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_ValidateTranPub>
		}
		static public void ValidateMergeSubscription()
		{
			//<snippetrmo_ValidateMergeSub>
			// Define the server, database, and publication names
			string publisherName = publisherInstance;
			string publicationName = "AdvWorksSalesOrdersMerge";
			string publicationDbName = "AdventureWorks2012";
			string subscriberName = subscriberInstance;
			string subscriptionDbName = "AdventureWorks2012Replica";

			MergePublication publication;

			// Create a connection to the Publisher.
			ServerConnection conn = new ServerConnection(publisherName);

			try
			{
				// Connect to the Publisher.
				conn.Connect();

				// Set the required properties for the publication.
				publication = new MergePublication();
				publication.ConnectionContext = conn;
				publication.Name = publicationName;
				publication.DatabaseName = publicationDbName;


				// If we can't get the properties for this merge publication, then throw an application exception.
				if (publication.LoadProperties())
				{
					// Initiate validation of the specified subscription.
					publication.ValidateSubscription(subscriberName,
						subscriptionDbName, ValidationOption.RowCountOnly);
					
					// Start the Merge Agent to synchronize and validate the subscription.
				}
				else
				{
					throw new ApplicationException(String.Format(
						"Settings could not be retrieved for the publication. " +
						"Ensure that the publication {0} exists on {1}.",
						publicationName, publisherName));
				}
			}
			catch (Exception ex)
			{
				// Do error handling here.
				throw new ApplicationException(String.Format(
					"The subscription at {0} to the {1} publication could not " +
					"be validated.", subscriberName, publicationName), ex);
			}
			finally
			{
				conn.Disconnect();
			}
			//</snippetrmo_ValidateMergeSub>
		}
        static public void CreateLogicalRecord()
        {
            //<snippetrmo_CreateLogicalRecord>
            // Define the Publisher and publication names.
            string publisherName = publisherInstance;
            string publicationName = "AdvWorksSalesOrdersMerge";
            string publicationDbName = "AdventureWorks2012";

            // Specify article names.
            string articleName1 = "SalesOrderHeader";
            string articleName2 = "SalesOrderDetail";
            
            // Specify logical record information.
            string lrName = "SalesOrderHeader_SalesOrderDetail";
            string lrClause = "[SalesOrderHeader].[SalesOrderID] = "
                + "[SalesOrderDetail].[SalesOrderID]";

            string schema = "Sales";
 
            MergeArticle article1 = new MergeArticle();
            MergeArticle article2 = new MergeArticle();
            MergeJoinFilter lr = new MergeJoinFilter();
            MergePublication publication = new MergePublication();

            // Create a connection to the Publisher.
            ServerConnection conn = new ServerConnection(publisherName);

            try
            {
                // Connect to the Publisher.
                conn.Connect();

                // Verify that the publication uses precomputed partitions.
                publication.Name = publicationName;
                publication.DatabaseName = publicationDbName;
                publication.ConnectionContext = conn;

                // If we can't get the properties for this merge publication, then throw an application exception.
                if (publication.LoadProperties())
                {
                    // If precomputed partitions is disabled, enable it.
                    if (publication.PartitionGroupsOption == PartitionGroupsOption.False)
                    {
                        publication.PartitionGroupsOption = PartitionGroupsOption.True;
                    }
                }
                else
                {
                    throw new ApplicationException(String.Format(
                        "Settings could not be retrieved for the publication. " +
                        "Ensure that the publication {0} exists on {1}.",
                        publicationName, publisherName));
                }

                // Set the required properties for the PurchaseOrderHeader article.
                article1.ConnectionContext = conn;
                article1.Name = articleName1;
                article1.DatabaseName = publicationDbName;
                article1.SourceObjectName = articleName1;
                article1.SourceObjectOwner = schema;
                article1.PublicationName = publicationName;
                article1.Type = ArticleOptions.TableBased;

                // Set the required properties for the SalesOrderDetail article.
                article2.ConnectionContext = conn;
                article2.Name = articleName2;
                article2.DatabaseName = publicationDbName;
                article2.SourceObjectName = articleName2;
                article2.SourceObjectOwner = schema;
                article2.PublicationName = publicationName;
                article2.Type = ArticleOptions.TableBased;

                if (!article1.IsExistingObject) article1.Create();
                if (!article2.IsExistingObject) article2.Create();

                // Define a logical record relationship between 
                // PurchaseOrderHeader and PurchaseOrderDetail. 

                // Parent article.
                lr.JoinArticleName = articleName1;
                
                // Child article.
                lr.ArticleName = articleName2;
                lr.FilterName = lrName;
                lr.JoinUniqueKey = true;
                lr.FilterTypes = FilterTypes.LogicalRecordLink;
                lr.JoinFilterClause = lrClause;

                // Add the logical record definition to the parent article.
                article1.AddMergeJoinFilter(lr);
            }
            catch (Exception ex)
            {
                // Do error handling here and rollback the transaction.
                throw new ApplicationException(
                    "The filtered articles could not be created", ex);
            }
            finally
            {
                conn.Disconnect();
            }
            //</snippetrmo_CreateLogicalRecord>
        }
        static public void CreateMergePullSubWebSyncAnon(string winLogin, string winPassword)
        {
            //<snippetrmo_CreateMergePullSub_WebSync_Anon>
            // The publication must support anonymous Subscribers, pull 
            // subscriptions, and Web synchronization.
            // Define the Publisher, publication, and databases.
            string publicationName = "AdvWorksSalesOrdersMerge";
            string publisherName = publisherInstance;
            string subscriberName = subscriberInstance;
            string subscriptionDbName = "AdventureWorks2012Replica";
            string publicationDbName = "AdventureWorks2012";
            string hostname = @"adventure-works\garrett1";
            string webSyncUrl = "https://" + publisherInstance + "/WebSync/replisapi.dll";

            //Create the Subscriber connection.
            ServerConnection conn = new ServerConnection(subscriberName);

            // Create the objects that we need.
            MergePullSubscription subscription;

            try
            {
                // Connect to the Subscriber.
                conn.Connect();

                // Define the pull subscription.
                subscription = new MergePullSubscription();
                subscription.ConnectionContext = conn;
                subscription.PublisherName = publisherName;
                subscription.PublicationName = publicationName;
                subscription.PublicationDBName = publicationDbName;
                subscription.DatabaseName = subscriptionDbName;
                subscription.HostName = hostname;

                // Specify an anonymous Subscriber type since we can't 
                // register at the Publisher with a direct connection.
                subscription.SubscriberType = MergeSubscriberType.Anonymous;

                // Specify the Windows login credentials for the Merge Agent job.
                subscription.SynchronizationAgentProcessSecurity.Login = winLogin;
                subscription.SynchronizationAgentProcessSecurity.Password = winPassword;

                // Enable Web synchronization.
                subscription.UseWebSynchronization = true;
                subscription.InternetUrl = webSyncUrl;

                // Specify the same Windows credentials to use when connecting to the
                // Web server using HTTPS Basic Authentication.
                subscription.InternetSecurityMode = AuthenticationMethod.BasicAuthentication;
                subscription.InternetLogin = winLogin;
                subscription.InternetPassword = winPassword;

                // Ensure that we create a job for this subscription.
                subscription.CreateSyncAgentByDefault = true;
                
                // Create the pull subscription at the Subscriber.
                subscription.Create();
            }
            catch (Exception ex)
            {
                // Implement the appropriate error handling here.
                throw new ApplicationException(String.Format(
                    "The subscription to {0} could not be created.", publicationName), ex);
            }
            finally
            {
                conn.Disconnect();
            }
            //</snippetrmo_CreateMergePullSub_WebSync_Anon>
        }
    }
}

