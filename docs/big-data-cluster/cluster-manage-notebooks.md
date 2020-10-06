---
title: Managing Big Data Clusters (BDC) with Jupyter notebooks and Azure Data Studio
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

# Managing Big Data Clusters (BDC) the cluster with notebooks

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to assist in managing Big Data Clusters.

Each notebook is designed to check for its own dependencies. A **Run all cells** will either complete successfully or will raise an exception with a hyperlinked hint to another notebook to resolve the missing dependency. Follow the hint hyperlink to the subsequent notebook, press **Run all cells**, and upon success return back to the original notebook, and **Run all cells**.

Once all dependencies are installed, but **Run all cells** fails, each notebook will analyze results and where possible, produce a hyperlinked hint to another notebook to further aid in resolving the issue.


## Installing and uninstalling utilities Big Data Cluster (BDC)

This section contains a set of notebooks useful for installing and uninstalling command line tools and packages needed to manage SQL Server Big Data Clusters.


<install> notebook table + <install-kubeadm-with-lpvs>




## Managing Certificates on Big Data Clusters (BDC)

A set of notebooks to run a notebook for managing Certificates on Big Data Clusters. 

|Name |Description |
|---|---|---|---|
|CER001 - Generate a Root CA certificate|Generate a Root CA certificate.Consider using one Root CA certificate for all non-production clusters in each environment, as this reduces the number of Root CA certificates that need to be uploaded to clients connecting to these clusters. |
|CER002 - Download existing Root CA certificate|Use this notebook to download a generated Root CA certificate from a cluster.|
|CER003 - Upload existing Root CA certificate|CER003 - Upload existing Root CA certificate.|
|CER004 - Download and Upload existing Root CA certificate|Download and Upload existing Root CA certificate. |
|CER010 - Install generated Root CA locally|This notebook will copy locally (from a Big Data Cluster) the generated Root CA certificate that was installed using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**, And then install the Root CA certificate into this machine’s local certificate store.|
|CER020 - Create Management Proxy certificate|UThis notebook creates a certificate for the Management Proxy endpoint.|
|CER021 - Create Knox certificate|This notebook creates a certificate for the Knox Gateway endpoint.|
|CER022 - Create App Proxy certificate|This notebook creates a certificate for the App Deploy Proxy endpoint.|
|CER023 - Create Master certificate|TThis notebook creates a certificate for the Master endpoint.|
|CER024 - Create Controller certificate|This notebook creates a certificate for the Controller endpoint. It creates a controller-privatekey.pem as the private key and controller-signingrequest.csr as the signing request.The private key is a secret. The signing request (CSR) will be used by the CA to generate a signed certificate for the service.|
|CER030 - Sign Management Proxy certificate with generated CA|This notebook signs the certificate created using **CER020 - Create Management Proxy certificate**with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**|
|CER031 - Sign Knox certificate with generated CA|This notebook signs the certificate created using **CER021 - Create Knox certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**|
|CER032 - Sign App-Proxy certificate with generated CA|This notebook signs the certificate created using **CER022 - Create App Proxy certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**.|
|CER033 - Sign Master certificate with generated CA|This notebook signs the certificate created using **CER023 - Create Master certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**.|
|CER034 - Sign Controller certificate with cluster Root CA|This notebook signs the certificate created using **CER024 - Create Controller certificate** with the generate Root CA Certificate, created using either **CER001 - Generate a Root CA certificate** or **CER003 - Upload existing Root CA certificate**.|
|CER040 - Install signed Management Proxy certificate|This notebook installs into the Big Data Cluster the certificate signed using **CER030 - Sign Management Proxy certificate with generated CA**.|
|CER041 - Install signed Knox certificate|This notebook installs into the Big Data Cluster the certificate signed using **CER031 - Sign Knox certificate with generated CA**.|
|CER042 - Install signed App-Proxy certificate|This notebook installs into the Big Data Cluster the certificate signed using **CER032 - Sign App-Proxy certificate with generated CA**.|
|CER044 - Install signed Controller certificate|This notebook installs into the Big Data Cluster the certificate signed using **CER034 - Sign Controller certificate with cluster Root CA** and please note that at the end of this notebook the Controller pod and all pods that use PolyBase (Master Pool and Compute Pool pods) will be restarted to load the new certificates.|
|CER050 - Wait for BDC to be Healthy|This notebook will wait until the Big Data Cluster has returned to a healthy state, after the Controller pod and pods that use PolyBase have been restarted to load the new certificates.|
|CER100 - Configure Cluster with Self Signed Certificates|This notebook will generate a new Root CA in the Big Data Cluster and create new certificates for each endpoint (Management, Gateway, App-Proxy and Controller). Sign each new certificate with the new generated Root CA, except the Controller cert (which is signed with the existing cluster Root CA), then install each certificate into the Big Data Cluster. Download the new generated Root CA into this machine’s Trusted Root Certification Authorities certificate store. All generated self-signed certificates will be stored in the controller pod (at the test_cert_store_root location).|
|CER101 - Configure Cluster with Self Signed Certificates using existing Root CA|This notebook will use an existing generated Root CA in the Big Data Cluster (uploaded with **CER003**) and create new certificates for each endpoint (Management, Gateway, App-Proxy and Controller), then sign each new certificate with the new generated Root CA, except the Controller cert (which is signed with the existing cluster Root CA), install each certificate into the Big Data Cluster. All generated self-signed certificates will be stored in the controller pod (at the test_cert_store_root location).Upon completion of this notebook, all https:// access to the Big Data Cluster from this machine (and any machine that installs the new Root CA) will show as being secure. The Notebook Runner chapter, will ensure CronJobs created (OPR003) to run App-Deploy will install the cluster Root CA to allow securely getting JWT tokens and the swagger.json.|

## Next steps

You can check out additional samples at [App Deploy Samples](https://aka.ms/sql-app-deploy).

For more information about [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
