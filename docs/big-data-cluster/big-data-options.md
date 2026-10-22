---
title: Big data options on the Microsoft SQL Server platform
titleSuffix: SQL Server
description: This article discusses migration strategies for SQL Server 2019 Big Clusters
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei, imotiwala
ms.date: 10/31/2025
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: concept-article
ms.update-cycle: 1095-days
ms.custom:
  - ignite-2025
monikerRange: ">=sql-server-ver15"
---
# Big data options on the Microsoft SQL Server platform

[!INCLUDE [SQL Server 2019 and later](../includes/applies-to-version/sqlserver2019-and-later.md)]

On February 28, 2025, [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] retired. **For more information, see [the announcement blog post](https://aka.ms/sqlserver_bigdataclusters).**

## Changes to PolyBase support in SQL Server

Related to the [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] retirement are some features related to scale out queries.

The PolyBase scale-out groups feature of Microsoft SQL Server has been retired. Scale-out group functionality is removed from the product in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. In-market versions of SQL Server 2019, SQL Server 2017, and SQL Server 2016, continue to support the functionality to the end of life of those products. PolyBase data virtualization continues to be fully supported as a scale-up feature in SQL Server.

Cloudera (CDP) and Hortonworks (HDP) Hadoop external data sources will also be retired for all in-market versions of SQL Server and aren't included in SQL Server 2022. Support for external data sources is limited to product versions in mainstream support by the respective vendor. You are advised encouraged to use the new object storage integration available in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)].

In [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and later versions, users must configure their external data sources to use new connectors when connecting to Azure Storage. The following table summarizes the change:

| External Data Source | From | To |
| :-- | :-- | :-- |
| Azure Blob Storage | `wasb[s]` | `abs` |
| ADLS Gen 2 | `abfs[s]` | `adls` |

> [!NOTE]  
> Azure Blob Storage (`abs`) will require usage of Shared Access Signature (SAS) for the SECRET in the database scoped credential. In SQL Server 2019 and earlier, the `wasb[s]` connector used Storage Account Key with database scoped credential when authenticating to Azure Storage account.

## Understand the Big Data Clusters architecture for replacement and migration options

To create your replacement solution for a Big Data storage and processing system, it's important to understand what [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] provided, and its architecture can help inform your choices. The architecture of a big data cluster was:

:::image type="content" source="media/big-data-options/architecture-diagram-overview.png" alt-text="Diagram that shows the SQL Server 2019 Big Data Clusters architecture overview." lightbox="media/big-data-options/architecture-diagram-overview.png":::

This architecture provided the following functionality mapping:

| Component | Benefit |
| :--- | :--- |
| **Kubernetes** | Open-source orchestrator for deploying and managing container-based applications at scale. Provides a declarative method to create and control resiliency, redundancy, and portability for the entire environment with elastic scale. |
| **Big Data Clusters Controller** | Provides management and security for the cluster. It contains the control service, the configuration store, and other cluster-level services such as Kibana, Grafana, and Elastic Search. |
| **Compute Pool** | Provides computational resources to the cluster. It contains nodes running SQL Server on Linux pods. The pods in the compute pool are divided into SQL Compute instances for specific processing tasks. This component also provides Data Virtualization using PolyBase to query external data sources without moving or copying the data. |
| **Data Pool** | Provides data persistence for the cluster. The data pool consists of one or more pods running SQL Server on Linux. It is used to ingest data from SQL queries or Spark jobs. |
| **Storage Pool** | The storage pool consists of storage pool pods comprised of SQL Server on Linux, Spark, and HDFS. All the storage nodes in a big data cluster are members of an HDFS cluster. |
| **App Pool** | Enables the deployment of applications on a big data cluster by providing interfaces to create, manage, and run applications. |

For more information on these functions, see [Introducing [!INCLUDE [big-data-cluster](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).

## Functionality replacement options for Big Data and SQL Server

The *operational* data function facilitated by SQL Server inside the Big Data Clusters can be replaced by SQL Server on premises in a hybrid configuration or using the Microsoft Azure platform. Microsoft Azure offers a choice of fully managed relational, NoSQL, and in-memory databases, spanning proprietary and open-source engines, to fit the needs of modern app developers. Infrastructure management—including scalability, availability, and security—is automated, saving you time and money, and allows you to focus on building applications while Azure-managed databases make your job simpler by surfacing performance insights through embedded intelligence, scaling without limits, and managing security threats. For more information, see [Azure databases](https://azure.microsoft.com/product-categories/databases/).

The next decision point is the locations of compute and data storage for *analytics*. The two architecture choices are in-cloud and hybrid deployments. Most analytic workloads can be migrated to the Microsoft Azure platform. Data "born in the cloud" (originated in Cloud-based applications) are prime candidates for these technologies, and data movement services can migrate large-scale on-premises data securely and quickly. For more on data movement options, see [Data transfer solutions](/azure/storage/common/storage-choose-data-transfer-solution).

Microsoft Azure has systems and certifications allowing secure data and data processing in various tools. For more information on these certifications, see the [Trust Center](https://www.microsoft.com/trust-center).

> [!NOTE]  
> The Microsoft Azure platform provides a very high level of security, multiple certifications for various industries, and honors data sovereignty for government requirements. Microsoft Azure also has a dedicated cloud platform for government workloads. Security alone should not be the primary decision point for on-premises systems. You should carefully evaluate the level of security provided by Microsoft Azure before deciding to retain your big data solutions on-premises.

In the **in-cloud architecture** option, all components reside in Microsoft Azure. Your responsibility lies with the data and code you create for storage and processing of your workloads. Those options are covered in more detail in this article.

- This option works best for a wide variety of components for storage and processing of data, and when you want to focus on data and processing constructs rather than infrastructure.

In the **hybrid architecture** options, some components are retained on-premises and others are placed in a Cloud Provider. Connectivity between the two is designed for the best placement of processing-over-data.

- This option works best when you have a considerable investment in on-premises technologies and architectures, but you wish to use the offerings of Microsoft Azure, or when you have either processing and application targets residing on-premises or for a worldwide audience.

For more information on building scalable architectures, see [Build a scalable system for massive data](/azure/architecture/data-guide/scenarios/build-scalable-database-solutions-azure-services).

## In-cloud

### Azure SQL plus Azure Machine Learning

You can replace the functionality of SQL Server Big Data Clusters by using one or more Azure SQL database options for operational data, and Microsoft Azure Machine Learning for your predictive workloads.

Azure Machine Learning is a cloud-based service that can be used for any kind of machine learning, from classical ML to deep learning, supervised, and unsupervised learning. Whether you prefer to write Python or R code with the SDK or work with no-code/low-code options in the studio, you can build, train, and track machine learning and deep-learning models in an Azure Machine Learning Workspace. With Azure Machine Learning, you can start training on your local machine and then scale out to the cloud. The service also interoperates with popular deep learning and reinforcement open-source tools such as PyTorch, TensorFlow, scikit-learn, and Ray RLlib.

Use Microsoft Azure Machine Learning as a replacement for [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] when you need:

- A designer-based web environment for Machine Learning: drag-n-drop modules to build your experiments and then deploy pipelines in a    low-code environment.
- Jupyter notebooks: use our example notebooks or create your own notebooks to use our SDK for Python samples for your machine    learning.
- R scripts or notebooks in which you use the SDK for R to write your own code or use the R modules in the designer.
- The [Many Models Solution Accelerator](https://github.com/microsoft/solution-accelerator-many-models) builds on Azure Machine Learning and enables you to train, operate, and manage hundreds or even thousands of machine learning models.
- Machine learning extensions for Visual Studio Code (preview) provide you with a full-featured development environment for building and managing your machine learning projects.
- A Machine learning Command-Line Interface (CLI), Azure Machine Learning includes an Azure CLI extension that provides commands for managing with Azure Machine Learning resources from the command line.
- Integration with open-source frameworks such as PyTorch, TensorFlow, and scikit-learn and many more for training, deploying, and managing the end-to-end machine learning process.
- Reinforcement learning with Ray RLlib.
- MLflow to track metrics and deploy models or Kubeflow to build end-to-end workflow pipelines.

The architecture of a Microsoft Azure Machine Learning deployment is as follows:

:::image type="content" source="media/big-data-options/azure-machine-learning-architecture.svg" alt-text="Diagram that shows Azure Machine Learning architecture of a workspace and its components.":::

For more information on Microsoft Azure Machine Learning, see [How Azure Machine Learning works](/azure/machine-learning/overview-what-is-azure-ml).

### Azure SQL from Databricks

You can replace the functionality of SQL Server Big Data Clusters by using one or more Azure SQL database options for operational data, and Microsoft Azure Databricks for your analytic workloads.

Azure Databricks is a data analytics platform optimized for the Microsoft Azure cloud services platform. Azure Databricks offers two environments for developing data intensive applications: Azure Databricks SQL Analytics and Azure Databricks Workspace.

Azure Databricks SQL Analytics provides an easy-to-use platform for analysts who want to run SQL queries on their data lake, create multiple visualization types to explore query results from different perspectives, and build and share dashboards.

Azure Databricks Workspace provides an interactive workspace that enables collaboration between data engineers, data scientists, and machine learning engineers. For a big data pipeline, the data (raw or structured) is ingested into Azure through Azure Data Factory in batches, or streamed near real-time using Apache Kafka, Event Hubs, or IoT Hub. This data lands in a data lake for long term persisted storage, in Azure Blob Storage or Azure Data Lake Storage. As part of your analytics workflow, use Azure Databricks to read data from multiple data sources and turn it into breakthrough insights using Spark.

Use Microsoft Azure Databricks as a replacement for [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] when you need:

- Fully managed Spark clusters with Spark SQL and DataFrames.
- Streaming for real-time data processing and analysis for analytical and interactive applications, Integrating with HDFS, Flume, and Kafka.
- Access to the MLlib library, consisting of common learning algorithms and utilities, including classification, regression, clustering, collaborative filtering, dimensionality reduction, and underlying optimization primitives.
- Documentation of your progress in notebooks in R, Python, Scala, or SQL.
- Visualization of data in a few steps, using familiar tools like Matplotlib, ggplot, or d3.
- Interactive dashboards to create dynamic reports.
- GraphX, for Graphs and graph computation for a broad scope of use cases from cognitive analytics to data exploration.
- Cluster creation in seconds, with dynamic autoscaling clusters, sharing them across teams.
- Programmatic cluster access using REST APIs.
- Instant access to the latest Apache Spark features with each release.
- A Spark Core API: Includes support for R, SQL, Python, Scala, and Java.
- An interactive workspace for exploration and visualization.
- Fully managed SQL endpoints in the cloud.
- SQL queries that run on fully managed SQL endpoints sized according to query latency and number of concurrent users.
- Integration with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)).
- Role-based access for fine-grained user permissions for notebooks, clusters, jobs, and data.
- Enterprise-grade SLAs.
- Dashboards for sharing insights, combining visualizations and text to share insights drawn from your queries.
- Alerts help you monitor and integrate, and notification when a field returned by a query meets a threshold. Use alerts to monitor your business or integrate them with tools to start workflows such as user onboarding or support tickets.
- Enterprise security, including Microsoft Entra ID integration, role-based controls, and SLAs that protect your data and your business.
- Integration with Azure services and Azure databases and stores including Synapse Analytics, Cosmos DB, Data Lake Store, and Blob storage.
- Integration with Power BI and other BI tools, such as Tableau Software.

The architecture of a Microsoft Azure Databricks deployment is as follows:

:::image type="content" source="media/big-data-options/azure-databricks-overview.png" alt-text="Diagram: architecture of an Azure Databricks workspace and its components and data flows, from people to applications.":::

For more information on Microsoft Azure Databricks, see [What is Databricks Data Science & Engineering?](/azure/databricks/scenarios/what-is-azure-databricks)

## Hybrid

<a id="fabric-mirrored-database"></a>

### Mirroring in Microsoft Fabric

As a data replication experience, [Database Mirroring in Fabric](../sql-server/fabric-database/fabric-mirrored-databases.md) is a low-cost and low-latency solution to bring data from various systems together into a single analytics platform. You can continuously replicate your existing data estate directly into Fabric's OneLake, including data from SQL Server 2016+, Azure SQL Database, Azure SQL Managed Instance, Oracle, Snowflake, Cosmos DB, and more.

With the most up-to-date data in a queryable format in OneLake, you can now use all the different services in Fabric, such as running analytics with Spark, executing notebooks, data engineering, visualizing through Power BI Reports, and more.

Mirroring in Fabric provides an easy experience to speed the time-to-value for insights and decisions, and to break down data silos between technology solutions, without developing expensive Extract, Transform, and Load (ETL) processes to move data.

With Mirroring in Fabric, you don't need to piece together different services from multiple vendors. Instead, you can enjoy a highly integrated, end-to-end, and easy-to-use product that is designed to simplify your analytics needs, and built for openness and collaboration between technology solutions that can read the open-source Delta Lake table format.

For more information, see:

- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Microsoft Fabric mirrored databases monitoring](/fabric/database/mirrored-database/monitor)
- [Explore data in your Mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)
- [What is Microsoft Fabric?](/fabric/get-started/microsoft-fabric-overview)
- [Model data in the default Power BI semantic model in Microsoft Fabric](/fabric/data-warehouse/model-default-power-bi-dataset)
- [What is the SQL analytics endpoint for a Lakehouse?](/fabric/data-engineering/lakehouse-sql-analytics-endpoint)
- [Direct Lake](/power-bi/enterprise/directlake-overview)

### Microsoft SQL Server on Windows, Apache Spark, and object storage on-premises

You can install SQL Server on Windows or Linux, and scale up the hardware architecture, using the [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] object-storage query capability and the PolyBase feature to enable queries across all data in your system.

Installing and configuring a scale-out platform such as Apache Hadoop or Apache Spark allows for querying non-relational data at scale. Using a central set of Object-Storage systems that support the S3-API allows both [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and Spark to access the same set of data across all systems.

You can also use the Kubernetes container orchestration system for your deployment. This allows a declarative-architecture that can run on-premises or in any Cloud that supports Kubernetes or the Red Hat OpenShift platform. To learn more about deploying SQL Server into a Kubernetes environment, see [Deploy a SQL Server container cluster on Azure](../linux/quickstart-sql-server-containers-azure.md) or watch [Deploying SQL Server 2019 in Kubernetes](/shows/data-exposed/deploying-sql-server-2019-in-kubernetes).

Use SQL Server and Hadoop/Spark on-premises as a replacement for [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] when you need to:

- Retain the entire solution on-premises
- Use dedicated hardware for all parts of the solution
- Access relational and non-relational data from the same architecture, in both directions
- Share a single set of non-relational data between SQL Server and the scale-out non-relational system

## Perform the migration

Once you pick a location (In-Cloud or Hybrid) for your migration, you should weigh the downtime and cost vectors to determine whether you run a new system and move the data from the previous system to the new in real-time (side-by-side migration) or a backup and restore, or a new-start of the system from existing data sources (in-place migration).

Your next decision is to either rewrite the current functionality in your system using the new architecture choice or move as much of the code as possible to the new system. While the former choice can take longer, it allows you to use the new methods, concepts, and advantages that the new architecture provides. In that case, the data access and functionality maps are the primary planning efforts you should focus on.

If you plan to migrate the current system with as little code-change as possible, the language compatibility is your primary focus for planning.

### Code migration

Your next step is to audit the code the current system uses and what changes it needs to run against the new environment.

There are two primary vectors for code migration to consider:

1. Sources and sinks
1. Functionality migration

#### Sources and sinks

The first task in code migration is to identify the data source connection methods, strings, or APIs that the code uses to access the data that is imported, its path, and its ultimate destination. Document those sources and create a map to the new architecture's locations.

- If the current solution is using a *pipeline* system to move the data through the system, map the new architecture sources, steps, and sinks to the pipeline's components.
- If the new solution is also replacing the *pipeline* architecture, treat the system as a new installation for planning purposes, even if you are reusing the hardware or cloud platform as the replacement.

### Functionality migration

The most complex work needed in a migration is to reference, update, or create the documentation of the functionality of the current system. If you are planning an in-place upgrade and attempting to reduce the amount of code rewrite as much as possible, this step takes the most time.

However, a migration from a previous technology is often an optimal time to update yourself on the latest advancements in technology and take advantage of the constructs that it provides. Often you can gain more security, performance, feature choices and even cost optimizations by a rewrite of your current system.

In either case, you have two primary factors involved in the migration: the code and languages the new system supports, and the choices around data movement. Usually, you should be able to change connection strings from the current big data cluster to the SQL Server instance and Spark environment. Any data connection information and the code cutover should be minimal.

If you are envisioning a rewrite of your current functionality, map the new libraries, packages, and DLLs to the architecture you chose for your migration. You'll find a list of each of the libraries, languages, and functions that each solution offers in the documentation references shown in the previous sections. Map out any suspect or unsupported languages and plan for the replacement with the chosen architecture.

### Data migration options

There are two common approaches for data movement in a large-scale analytic system. The first is to create a "cutover" process where the original system continues processing data, and that data is rolled up into a smaller set of aggregated report-data source. The new system then starts with fresh data and is used from the migration date onward.

In some cases, all data needs to move from the legacy system to the new system. In this case, you can mount the original file stores from SQL Server Big Data Clusters if the new system supports it and then copy the data piecewise to the new system, or you can create a physical move.

Migrating your current data from [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] to another system is highly dependent on two factors: the location of your current data, and the destination being on-premises or to-cloud.

#### On-premises data migration

For on-premises to on-premises migrations, you can migrate the SQL Server data with a backup and restore strategy, or you can set up replication to move some or all your relational data. SQL Server Integration Services can also be used to copy data from SQL Server to another location. For more information about moving data with SSIS, see [SQL Server Integration Services](../integration-services/sql-server-integration-services.md).

For the HDFS data in your current SQL Server Big Data Cluster environment, the standard approach is to mount the data to a stand-alone Spark Cluster, and either use the Object Storage process to move the data so that a [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] instance can access it or leave it as-is and continue to process it with Spark Jobs.

#### In-cloud data migration

For data located in cloud storage or on premises, you can use the Azure Data Factory, which has over 90 connectors for a full pipeline of transfer, with scheduling, monitoring, alerting, and other services. For more information on Azure Data Factory, see [What is Azure Data Factory?](/azure/data-factory/introduction)

If you would like to move large amounts of data securely and quickly from your local data estate to Microsoft Azure, you can use the Azure Import/Export Service. The Azure Import/Export service is used to securely import large amounts of data to Azure Blob storage and Azure Files by shipping disk drives to an Azure datacenter. This service can also be used to transfer data from Azure Blob storage to disk drives and ship to your on-premises sites. Data from one or more disk drives can be imported either to Azure Blob storage or Azure Files. For extremely large amounts of data, using this service can be the fastest path.

If you want to transfer data using disk drives supplied by Microsoft, you can use Azure Data Box Disk to import data into Azure. For more information, see [What is Azure Import/Export service?](/azure/import-export/storage-import-export-service)

For more information on these choices and the decisions that accompany them, see [Using Azure Data Lake Storage Gen1 for big data requirements](/azure/data-lake-store/data-lake-store-data-scenarios).

## Related content

- [Microsoft Azure Big data architecture guide](/azure/architecture/data-guide/big-data/)
- [Data Architecture discussion space](https://techcommunity.microsoft.com/t5/data-architecture/ct-p/DataArchitecture)
