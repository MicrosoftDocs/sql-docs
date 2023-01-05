---
title: Publish a Database Project for Azure SQL Database to the local emulator
description: Learn how to publish a Database Project for Azure SQL Database to the local emulator.
author: scoriani
ms.author: scoriani
ms.reviewer: mathoma
ms.date: 05/24/2022
ms.service: sql-database
ms.topic: how-to
ms.custom: template-how-to
---

# Publish a Database Project for Azure SQL Database to the local emulator 
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article provides steps to build and publish a Database Project to the [Azure SQL Database emulator (preview)](local-dev-experience-sql-database-emulator.md). 

## Overview

The Azure SQL Database [local development experience](local-dev-experience-overview.md) allow users to source control Database Projects and work offline when needed. The local development experience uses the [Azure SQL Database emulator](local-dev-experience-sql-database-emulator.md), a containerized database with close fidelity with the Azure SQL Database public service, as runtime host for Database Projects that can be published and tested locally as part of developer's inner loop. This article describes how to publish a Database Project to the local emulator.

## Prerequisites

Before you can publish a Database Project to the local emulator, you must:

- Follow the steps in [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md) to configure your environment.
- Create a Database Project by following the steps in [Create a SQL Database Project for a local Azure SQL Database development environment](local-dev-experience-create-database-project.md).

## Build and publish a Database Project

You must first build your Database Project before publishing. To complete this process:

1. First, follow the steps in [Build a Database Project](/sql/azure-data-studio/extensions/sql-database-project-extension-build#publish-to-an-existing-sql-instance).
1. Then follow the steps in [Publish the SQL project and deploy to a local Container](/sql/azure-data-studio/extensions/sql-database-project-extension-build#publish-the-sql-project-and-deploy-to-a-local-container).

## Next steps

Learn more about the local development experience for Azure SQL Database:

- [What is the local development experience for Azure SQL Database?](local-dev-experience-overview.md)
- [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md)
- [Create a Database Project for a local Azure SQL Database development environment](local-dev-experience-create-database-project.md)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart.md)
- [Introducing the Azure SQL Database emulator](local-dev-experience-sql-database-emulator.md)
