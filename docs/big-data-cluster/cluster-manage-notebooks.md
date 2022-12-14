---
title: Big Data Cluster Management notebooks
description: Manage Big Data Clusters with Jupyter notebooks and Azure Data Studio on SQL Server 2019.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 6/21/2022
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: kr2b-contr-experiment
ms.metadata: seo-lt-2019
---

# An Index of Management notebooks for SQL Server Big Data Clusters

This page is an index of notebooks for SQL Server Big Data Clusters. These executable notebooks (.ipynb) manage Big Data Clusters for SQL Server 2019.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

You can manage SQL Server Big Data Clusters with Jupyter notebooks. When you do, all notebooks check for their own dependencies. The **Run all cells** option either completes successfully or raises an exception with a hyperlinked *hint* to another notebook to resolve the missing dependency. Follow the hint hyperlink to the target notebook, click **Run all cells**. Upon successful completion, return to the original notebook and select **Run all cells**.

When all dependencies are installed and **Run all cells** fails, each notebook analyzes the results. Where possible, a hyperlinked hint gives direction on how to resolve the issue.

* For more information on using notebooks to manage SQL Server Big Data Clusters, see [Manage SQL Server Big Data Clusters with Azure Data Studio notebooks](notebooks-manage-bdc.md).
* For the location of big data cluster administration notebooks, see [Where to find SQL Server Big Data Clusters administration notebooks](view-cluster-status.md#where-to-find--administration-notebooks).

## Install and uninstall utilities on Big Data Clusters

The following is a set of notebooks that are useful for installing and uninstalling command-line tools. They also provide packages to manage SQL Server Big Data Clusters.

|Name |Description |
|---|---|
|SOP012 - Install unixodbc for Mac|Use this notebook when getting errors while using brew install the odbc for SQL Server.|
|SOP036 - Install kubectl command-line interface|Use this notebook to install kubectl command-line interface regardless your OS.|
|SOP037 - Uninstall kubectl command-line interface|Use this notebook to uninstall kubectl command-line interface regardless your OS.|
|SOP038 - Install Azure command-line interface|Use this notebook to install Azure CLI command-line interface regardless your OS.|
|SOP040 - Upgrade pip in ADS Python sandbox|Use this notebook to upgrade pip in ADS Python sandbox.|
|SOP059 - Install Kubernetes Python module|Use this notebook to install Kubernetes modules with Python.|
|SOP060 - Uninstall kubernetes module|Use this notebook to uninstall Kubernetes modules with Python.|
|SOP062 - Install ipython-sql and pyodbc modules|Use this notebook to install ipython-sql and pyodbc modules.|
|SOP069 - Install ODBC for SQL Server|Use this notebook to install ODBC driver since some subcommands in azdata require the SQL Server ODBC driver.|

## Backup and restore Big Data Clusters

The following is a set of notebooks that are useful for backup and restore operations on SQL Server Big Data Clusters.

| Name | Description |
|--|--|
| SOP008 - Back up HDFS files to Azure Data Lake Store Gen2 with distcp | This Standard Operating Procedure (SOP) backs up data from the source big data cluster's HDFS filesystem to the Azure Data Lake Store Gen2 account you specify. Please ensure the Azure Data Lake Store Gen2 account is configured with "hierarchical namespace" enabled. |

## Manage Certificates on Big Data Clusters

The following is a set of notebooks to manage Certificates on Big Data Clusters.

|Name |Description |
|---|---|
|CER001 - Generate a Root CA certificate|Generate a Root CA certificate. Consider using one Root CA certificate for all non-production clusters in each environment, as this technique reduces the number of Root CA certificates that need to be uploaded to clients connecting to these clusters. |
|CER002 - Download existing Root CA certificate|Use this notebook to download a generated Root CA certificate from a cluster.|
|CER003 - Upload existing Root CA certificate|CER003 - Upload existing Root CA certificate.|
|CER004 - Download and Upload existing Root CA certificate|Download and Upload existing Root CA certificate. |
|CER005 - Install a new Root CA certificate|Installs a new Root CA certificate. |
|CER010 - Install generated Root CA locally|This notebook will copy locally (from a big data cluster) the generated Root CA certificate that was installed using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**, And then install the Root CA certificate into this machine's local certificate store.|
|CER020 - Create Management Proxy certificate|This notebook creates a certificate for the Management Proxy endpoint.|
|CER021 - Create Knox certificate|This notebook creates a certificate for the Knox Gateway endpoint.|
|CER022 - Create App Proxy certificate|This notebook creates a certificate for the App Deploy Proxy endpoint.|
|CER023 - Create Master certificate|This notebook creates a certificate for the Master endpoint.|
|CER024 - Create Controller certificate|This notebook creates a certificate for the Controller endpoint.|
|CER025 - Upload existing Management Proxy certificate|This notebook uploads an existing Management Proxy certificate.|
|CER026 - Upload existing Gateway certificate|This notebook uploads an externally generated Gateway certificate to a cluster.|
|CER027 - Upload existing App Service Proxy certificate|This notebook uploads an externally generated App Service certificate to a cluster.|
|CER028 - Upload existing Master certificates|This notebook uploads an externally generated Master certificate to a cluster.|
|CER028 - Upload existing Controller certificate|This notebook uploads an externally generated Controller certificate to a cluster.|
|CER030 - Sign Management Proxy certificate with generated CA|This notebook signs the certificate created using **CER020 - Create Management Proxy certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**|
|CER031 - Sign Knox certificate with generated CA|This notebook signs the certificate created using **CER021 - Create Knox certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**|
|CER032 - Sign App-Proxy certificate with generated CA|This notebook signs the certificate created using **CER022 - Create App Proxy certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**.|
|CER033 - Sign Master certificate with generated CA|This notebook signs the certificate created using **CER023 - Create Master certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**.|
|CER034 - Sign Controller certificate with generated CA|This notebook signs the certificate created using **CER024 - Create Controller certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**.|
|CER040 - Install signed Management Proxy certificate|This notebook installs into the big data cluster the certificate signed using **CER030 - Sign Management Proxy certificate with generated CA**.|
|CER041 - Install signed Knox certificate|This notebook installs into the big data cluster the certificate signed using **CER031 - Sign Knox certificate with generated CA**.|
|CER042 - Install signed App-Proxy certificate|This notebook installs into the big data cluster the certificate signed using **CER032 - Sign App-Proxy certificate with generated CA**.|
|CER043 - Install signed Master certificate|This notebook installs into the big data cluster the certificate signed using **CER033 - Sign Master certificate with cluster Root CA** and note that at the end of this notebook the Master pods will be restarted to load the new certificates.|
|CER044 - Install signed Controller certificate|This notebook installs into the big data cluster the certificate signed using **CER034 - Sign Controller certificate with cluster Root CA** and note that at the end of this notebook the Controller pod and all pods that use PolyBase (Master Pool and Compute Pool pods) will be restarted to load the new certificates.|
|CER050 - Wait for BDC to be Healthy|This notebook will wait until the big data cluster has returned to a healthy state, after the Controller pod and pods that use PolyBase have been restarted to load the new certificates.|
|CER100 - Configure Cluster with self-signed Certificates|This notebook will generate a new Root CA in the big data cluster and create new certificates for each endpoint (those endpoints are : Management, Gateway, App-Proxy, and Controller). Sign each new certificate with the new generated Root CA, except the Controller cert (which is signed with the existing cluster Root CA), then install each certificate into the big data cluster. Download the new generated Root CA into this machine's Trusted Root Certification Authorities certificate store. All generated self-signed certificates will be stored in the controller pod at the test_cert_store_root location.|
|CER101 - Configure Cluster with self-signed Certificates using existing Root CA|This notebook will use an existing generated Root CA in the big data cluster (uploaded with **CER003**) and create new certificates for each endpoint (Management, Gateway, App-Proxy and Controller), then sign each new certificate with the new generated Root CA, except the Controller cert (which is signed with the existing cluster Root CA), install each certificate into the big data cluster. All generated self-signed certificates will be stored in the controller pod (at the test_cert_store_root location). Upon completion of this notebook, all https:// access to the big data cluster from this machine (and any machine that installs the new Root CA) will show as being secure. The Notebook Runner chapter, will ensure CronJobs created (OPR003) to run App-Deploy will install the cluster Root CA to allow securely getting JWT tokens and the swagger.json.|
|CER102 - Configure Cluster with Self Signed Certificates using existing Big Data Cluster CA|This notebook will configure the cluster with self signed certificates using and existing Big Data Cluster CA. Read the notebook for detailed information.|
|CER103 - Configure Cluster with externally signed certificates|The purpose of this notebook is to rotate the endpoint certificates with the ones generated and signed outside of the big data cluster. Read the notebook for detailed information.|

## Encryption at Rest utilities on Big Data Clusters

This section contains a set of notebooks to manage Encryption at Rest on BDC.

|Name |Description |
|---|---|
|SOP0124 - List Keys for Encryption at Rest|Use this notebook list all HDFS keys.|
|SOP0128 - Enable HDFS Encryption zones in Big Data Clusters|Use this notebook to enable HDFS Encryption Zones when upgrading to CU8 from CU6 or previous. Not required on new deployments of CU8+ or when upgrading to CU9.|
|SOP0125 - Delete Key For Encryption at Rest|Use this notebook to delete HDFS encryption zone keys. __Caution!__|
|SOP0126 - Backup Keys For Encryption at Rest|Use this notebook to backup HDFS encryption zone keys.|
|SOP0127 - Restore Keys For Encryption at Rest|Use this notebook to restore HDFS encryption zone keys.|

## Password rotation

Notebooks to manage password rotation on Big Data Clusters.

|Name |Description |
|---|---|
|PASS001 - Update Administrator Domain Controller Password |This notebook assumes the DSA password is already updated in the Domain Controller. Run this notebook with the given parameters to update the Big Data Cluster with the new DSA password. This will restart the controller pod. |

## Next steps

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).
