---
title: Security concepts for SQL Server Big Data Cluster | Microsoft Docs
description:
author: negust 
ms.author: negust 
manager: craigg
ms.date: 08/06/2018
ms.topic: conceptual
ms.prod: sql
---

# Security concepts for SQL Server Big Data Cluster

## Cluster Endpoints

There are three entry points to the Big Data Cluster
 
* HDFS/Spark gateway – This is an HTTPS-based endpoint. Other endpoints are proxied through this. HDFS/Spark gateway is used for accessing services like webHDFS and Livy.

* Controller endpoint – Big Data Cluster  management service that exposes REST APIs for managing the cluster. Some tools like the Admin portal is also accessed through this endpoint.

* Master Instance  - TDS endpoint for database tools and applications to connect to SQL Server Master Instance in the cluster.

Currently, there is no option of opening up additional ports for accessing the cluster from the outside.

### How endpoints are secured

Securing endpoints in the Big Data Cluster is done using passwords that can be set/updated either using environment variables or CLI commands. All cluster internal passwords are stored as Kubernetes secrets.  

In order to update Knox and controller admin passwords, you can use the following commands:

    TODO: Provide example of updating Knox and Controller Password

It is not possible to change the internal controller SQL Server password. This password is set using environment variables at the time of deployment.

## Intra cluster secure communication

Communication with non-SQL services within the Big Data Cluster, like for example Livy to Spark or SPark to Storage Pool, is secured using certificates. All SQL Sever to SQL Server communication between is secured using SQL logins.

## Authentication

Before provisioning the cluster, the Big Data Cluster administrator sets credentials for Controller and cluster provisioning. These credentials will be stored in the Controller service.  

Upon provisioning if the Controller, a number of SQL logins are created:

* A special SQL login is created in the Controller SQL instance that is system managed, with sysadmin role. The password for this login is captured as a K8s secret.

* A sysadmin login is created in all SQL instances in the cluster, that Controller owns and manages. It is required for Controller to perform administrative tasks on these instances (i.e. HA setup, upgrade etc.). These logins are also used for intra cluster communication between SQL instances, i.e. Master instance communicating with Data Pool.

## Next steps

TBD
