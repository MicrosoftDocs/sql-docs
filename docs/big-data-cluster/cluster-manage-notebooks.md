---
title: Manage Big Data Clusters (BDC) with Jupyter notebooks and Azure Data Studio
titleSuffix: SQL Server Big Data Clusters
description: Managing Big Data Clusters (BDC) with Jupyter notebooks and Azure Data Studio on SQL Server 2019 big data cluster.
author: cloudmelon
ms.author: melqin
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 09/22/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Manage Big Data Clusters (BDC) the cluster with notebooks

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to assist in managing Big Data Clusters.

Each notebook is designed to check for its own dependencies. A **Run all cells** will either complete successfully or will raise an exception with a hyperlinked hint to another notebook to resolve the missing dependency. Follow the hint hyperlink to the subsequent notebook, press **Run all cells**, and upon success return back to the original notebook, and **Run all cells**.

Once all dependencies are installed, but **Run all cells** fails, each notebook will analyze results and where possible, produce a hyperlinked hint to another notebook to further aid in resolving the issue.


## Installing and uninstalling utilities on Big Data Cluster (BDC)

This section contains a set of notebooks useful for installing and uninstalling command-line tools and packages needed to manage SQL Server Big Data Clusters.

|Name |Description |
|---|---|
|SOP010 - Upgrade a big data cluster|Use this notebook to upgrade a Big Data Cluster using azdata. |
|SOP012 - Install unixodbc for Mac|Use this notebook when getting errors while using brew install the odbc for SQL Server.|
|SOP036 - Install kubectl command-line interface|Use this notebook to install kubectl command-line interface regardless your OS.|
|SOP037 - Uninstall kubectl command-line interface|Use this notebook to uninstall kubectl command-line interface regardless your OS.|
|SOP038 - Install Azure command-line interface|Use this notebook to install Azure CLI command-line interface regardless your OS.|
|SOP039 - Uninstall Azure command-line interface|Use this notebook to uninstall Azure CLI command-line interface regardless your OS.|
|SOP040 - Upgrade pip in ADS Python sandbox|Use this notebook to upgrade pip in ADS Python sandbox.|
|SOP054 - Install azdata command-line interface|Use this notebook to install azdata command-line interface regardless your OS.|
|SOP055 - Uninstall azdata command-line interface|Use this notebook to uninstall azdata command-line interface regardless your OS.|
|SOP059 - Install Kubernetes Python module|Use this notebook to install Kubernetes modules with Python.|
|SOP060 - Uninstall kubernetes module|Use this notebook to uninstall Kubernetes modules with Python.|
|SOP061 - Delete a big data cluster|Use this notebook to delete a BDC cluster.|
|SOP062 - Install ipython-sql and pyodbc modules|Use this notebook to install ipython-sql and pyodbc modules.|
|SOP063 - Install azdata CLI (using package manager)|Use this notebook to install azdata CLI (using package manager).|
|SOP064 - Uninstall azdata CLI (using package manager)|Use this notebook to uninstall azdata CLI (using package manager).|
|SOP069 - Install ODBC for SQL Server|Use this notebook to install ODBC driver since some subcommands in azdata require the SQL Server ODBC driver.|

## Encryption at Rest utilities on Big Data Cluster (BDC)

This section contains a set of notebooks useful for managing Encryption at Rest features on BDC.

|Name |Description |
|---|---|
|SOP0124 - List Keys for Encryption at Rest|Use this notebook list all HDFS keys.|
|SOP0128 - Enable HDFS Encryption zones in Big Data Clusters|Use this notebook to enable HDFS Encryption Zones when upgrading to CU8 from CU6 or previous. Not required on new deployments of CU8+ or when upgrading to CU9.|
|SOP0125 - Delete Key For Encryption at Rest|Use this notebook to delete HDFS encryption zone keys. __Caution!__|
|SOP0126 - Backup Keys For Encryption at Rest|Use this notebook to backup HDFS encryption zone keys.|
|SOP0127 - Restore Keys For Encryption at Rest|Use this notebook to restore HDFS encryption zone keys.|

## Managing Certificates on Big Data Clusters (BDC)

A set of notebooks to run a notebook for managing Certificates on Big Data Clusters.

|Name |Description |
|---|---|
|CER001 - Generate a Root CA certificate|Generate a Root CA certificate. Consider using one Root CA certificate for all non-production clusters in each environment, as this technique reduces the number of Root CA certificates that need to be uploaded to clients connecting to these clusters. |
|CER002 - Download existing Root CA certificate|Use this notebook to download a generated Root CA certificate from a cluster.|
|CER003 - Upload existing Root CA certificate|CER003 - Upload existing Root CA certificate.|
|CER004 - Download and Upload existing Root CA certificate|Download and Upload existing Root CA certificate. |
|CER010 - Install generated Root CA locally|This notebook will copy locally (from a Big Data Cluster) the generated Root CA certificate that was installed using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**, And then install the Root CA certificate into this machine’s local certificate store.|
|CER020 - Create Management Proxy certificate|This notebook creates a certificate for the Management Proxy endpoint.|
|CER021 - Create Knox certificate|This notebook creates a certificate for the Knox Gateway endpoint.|
|CER022 - Create App Proxy certificate|This notebook creates a certificate for the App Deploy Proxy endpoint.|
|CER023 - Create Master certificate|This notebook creates a certificate for the Master endpoint.|
|CER030 - Sign Management Proxy certificate with generated CA|This notebook signs the certificate created using **CER020 - Create Management Proxy certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**|
|CER031 - Sign Knox certificate with generated CA|This notebook signs the certificate created using **CER021 - Create Knox certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**|
|CER032 - Sign App-Proxy certificate with generated CA|This notebook signs the certificate created using **CER022 - Create App Proxy certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**.|
|CER033 - Sign Master certificate with generated CA|This notebook signs the certificate created using **CER023 - Create Master certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**.|
|CER040 - Install signed Management Proxy certificate|This notebook installs into the Big Data Cluster the certificate signed using **CER030 - Sign Management Proxy certificate with generated CA**.|
|CER041 - Install signed Knox certificate|This notebook installs into the Big Data Cluster the certificate signed using **CER031 - Sign Knox certificate with generated CA**.|
|CER042 - Install signed App-Proxy certificate|This notebook installs into the Big Data Cluster the certificate signed using **CER032 - Sign App-Proxy certificate with generated CA**.|
|CER043 - Install signed Controller certificate|This notebook installs into the Big Data Cluster the certificate signed using **CER034 - Sign Controller certificate with cluster Root CA** and note that at the end of this notebook the Controller pod and all pods that use PolyBase (Master Pool and Compute Pool pods) will be restarted to load the new certificates.|
|CER050 - Wait for BDC to be Healthy|This notebook will wait until the Big Data Cluster has returned to a healthy state, after the Controller pod and pods that use PolyBase have been restarted to load the new certificates.|
|CER100 - Configure Cluster with self-signed Certificates|This notebook will generate a new Root CA in the Big Data Cluster and create new certificates for each endpoint (those endpoints are : Management, Gateway, App-Proxy, and Controller). Sign each new certificate with the new generated Root CA, except the Controller cert (which is signed with the existing cluster Root CA), then install each certificate into the Big Data Cluster. Download the new generated Root CA into this machine’s Trusted Root Certification Authorities certificate store. All generated self-signed certificates will be stored in the controller pod at the test_cert_store_root location.|
|CER101 - Configure Cluster with self-signed Certificates using existing Root CA|This notebook will use an existing generated Root CA in the Big Data Cluster (uploaded with **CER003**) and create new certificates for each endpoint (Management, Gateway, App-Proxy and Controller), then sign each new certificate with the new generated Root CA, except the Controller cert (which is signed with the existing cluster Root CA), install each certificate into the Big Data Cluster. All generated self-signed certificates will be stored in the controller pod (at the test_cert_store_root location). Upon completion of this notebook, all https:// access to the Big Data Cluster from this machine (and any machine that installs the new Root CA) will show as being secure. The Notebook Runner chapter, will ensure CronJobs created (OPR003) to run App-Deploy will install the cluster Root CA to allow securely getting JWT tokens and the swagger.json.|

## Backup and restore from Big Data Cluster (BDC)

This section contains a set of notebooks useful for backup and restore operations for SQL Server Big Data Clusters.

| Name | Description |
|--|--|
| SOP008 - Back up HDFS files to Azure Data Lake Store Gen2 with distcp | This Standard Operating Procedure (SOP) backs up data from the source SQL Server 2019 BDC cluster’s HDFS filesystem to the Azure Data Lake Store Gen2 account you specify. Please ensure the Azure Data Lake Store Gen2 account is configured with “hierarchical namespace” enabled. |

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
