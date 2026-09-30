---
title: SQL Server on Linux Containers
description: A comparison of virtual machines vs containers, and guidance for containerized SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/13/2026
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - linux-related-content
---
# SQL Server on Linux containers

[!INCLUDE [sql-linux](../../includes/applies-to-version/sql-linux.md)]

Containers provide a lightweight and portable way to run [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux. Compared to virtual machines (VMs), containers start faster and simplify lifecycle management while still providing isolation for database workloads.

## Why use containers

Containers share the host operating system (OS) rather than running a full guest OS, which reduces overhead and enables faster provisioning. On Linux hosts, you can run [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] or later versions as containers. You can run these containers by using Docker or other supported container runtimes, either standalone or managed by an orchestrator.

Containers are well suited for scenarios that require rapid deployment, consistency across environments, and simplified operations:

- Containers start and scale faster than virtual machines.
- A single host OS reduces administrative overhead.
- Identical images produce consistent deployments across development, test, and production.
- A smaller footprint enables higher density on shared infrastructure.

Common use cases include development and testing, CI/CD pipelines, and cloud-native or scalable architectures.

## Get started

To begin working with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux containers, see the following resources:

### Install and configure

- [Quickstart: Run SQL Server Linux container images with Docker](../install-upgrade/quickstart-install-docker.md)
- [Deploy and connect to SQL Server Linux containers](deploy.md)
- [Configure and customize SQL Server Linux containers](configure.md)
- [Install SQL Server Machine Learning Services (Python and R) on Docker](../install-upgrade/setup-machine-learning-docker.md)

### Security and authentication

- [Secure SQL Server Linux containers](security.md)
- [Tutorial: Configure Active Directory authentication with SQL Server on Linux containers](tutorial-adutil.md)

### High availability

- [High availability for SQL Server containers](../business-continuity/containers/high-availability-overview.md)

### Operations and troubleshooting

- [How to use distributed transactions with SQL Server Linux containers](configure-distributed-transactions.md)
- [Troubleshoot SQL Server Docker containers](troubleshoot.md)

## Orchestration

For production deployments, use an orchestrator such as Kubernetes, Azure Kubernetes Service (AKS), or Red Hat OpenShift to manage containers. Orchestrators handle scheduling, scaling, health monitoring, and recovery.

Microsoft provides guidance and tooling for running [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] containers on Kubernetes, including supported container images and deployment examples.

- [Quickstart: Deploy a SQL Server container cluster on Azure or Red Hat OpenShift](../install-upgrade/quickstart-containers-azure.md)
- [Quickstart: Deploy a SQL Server Linux container to Kubernetes using Helm charts](kubernetes-deploy-helm-charts.md)
- [Deploy availability groups on Kubernetes with DH2i DxOperator on Azure Kubernetes Service](../business-continuity/containers/tutorial-kubernetes-dxoperator.md)

## Storage considerations

Databases running in containers need *persistent storage* that exists outside the container lifecycle. On Kubernetes, you typically use persistent volumes and persistent volume claims (PVCs) to provide this storage.

A typical deployment includes:

- A persistent volume to store database files.
- A deployment or StatefulSet that uses the Microsoft [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Linux container image.
- A service or load balancer that provides stable network access.

With this configuration, the orchestrator can automatically restart or replace containers if a node fails, and your data is preserved.

## Related content

- [What is SQL Server on Linux?](../sql-server-linux-overview.md)
- [Deploy SQL Server Linux containers on Kubernetes with StatefulSets](kubernetes-best-practices-statefulsets.md)
- [Configure SQL Server on Linux with the mssql-conf tool](../configure/mssql-conf.md)
