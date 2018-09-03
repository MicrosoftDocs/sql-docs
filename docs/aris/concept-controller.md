---
title: Overview of the SQL Server Aris controller | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/06/2018
ms.topic: conceptual
ms.prod: sql
---

# Overview of the SQL Server Aris controller

## What is the cluster Controller?
The Controller hosts the core logic for building and managing the cluster. It takes care of all interactions with Kubernetes, SQL Servers instances that are part of the cluster as well as Hadoop components like HDFS. 

The Controller service provides the following core functionality: 
- Manage cluster lifecycle: cluster bootstrap & delete, update configurations and upgrade (upgrade not available in CTP2.0)
- Manage master SQL Server instances
- Manage compute, data and storage pools
- Expose monitoring tools to observe the state of the cluster
- Expose troubleshooting tools to detect and repair unexpected issues
- Manage cluster security: ensure secure cluster endpoints, manage users and roles, configure credentials for intra-cluster communication
- Manage the workflow of upgrades so that they are implemented safely (not available in CTP2.0)
- Manage high availability and DR for statefull services in the cluster (not in CTP2.0)

## Deploying the Controller service
The Controller is hosted as a daemon set in the same Kubernetes environment where the customer wants to build out the cluster. This service is installed by a Kubernetes administrator during cluster bootstrap, using the mssqlctl command-line utility:

```bash
python mssqlctl.py create cluster <name of your cluster>
```

The buildout workflow will layout on top of Kubernetes a fully functional Aris cluster that includes all the components described in the Overview (TBD add link) section. This workflow creates first the Controller, and once this is deployed, it will coordinate the installation and configuration of rest of the services part of Master, Compute, Data and Storage pools.

## Manging the cluster through the Controller
Customers are expected to manage the cluster purely through the Controller using either `mssqlctl` APIs or the administration portal this is hosted within the cluster. If customers will deploy additional Kubernetes objects (e.g. pods) into the same namespace, they will not be managed or monitored by the Controller.
The Controller and the Kubernetes objects (stateful sets, pods, secrets, etc…) created for the cluster reside in a dedicated Kubernetes namespace. The Controller service will be granted permission by the Kubernetes cluster administrator to manage all resources within that namespace.  The RBAC policy this scenario is configured automatically as part of initial cluster deployment. 

### mssqlctl
`mssqlctl` is a command line utility written in Python that enables cluster administrators to bootstrap and manage the Aris cluster via REST APIs.
TBD Aquisition experience I.e. from any client machine->pip install.... 

### Cluster Admin Portal
Once the Controller service is up and running, cluster administrator can leverage the new Cluster Admin Portal (TBD link to the corresponding topic) to monitor the deployment progress, detect and troubleshoot issues with services within the cluster. 

## Monitoring and troubleshooting Controller service
TBD (see service status, where are the logs)

## Controller service security
All communication to the Controller is conducted via a REST API over HTTPS. Certificates for Controller endpoint will be configured at bootstrap time. A self-signed certificate will be automatically generated for the CTP2.0. In future releases,  we will provide a mechanism for customers to provide their certificates from their own certificate authority for production deployments. 

Authentication to Controller endpoint is based on username and password. These credentials are provisioned at cluster bootstrap time using the input for environment variables `CONTROLLER_USERNAME` and `CONTROLLER_PASSWORD`. For example, from a Linux client, you can run below script to set the environment variables:

```bash
export CONTROLLER_USERNAME=<your controller user name>
export CONTROLLER_PASSWORD=<you controller password>
```

> [!NOTE]
> You must provide a password that is in compliance with [SQL Server password complexity requirements](https://docs.microsoft.com/en-us/sql/relational-databases/security/password-policy?view=sql-server-2017).
 
`mssqlctl` provides an API to rotate the Controller password. For example, run below script to update the controller password:

```bash
TBD
```

## Next steps

- [Deploy SQL Server Aris on Kubernetes](quickstart-sql-server-aris-deploy.md)
