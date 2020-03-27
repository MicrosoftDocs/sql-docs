---
title: Data exploration using Python 
description: Learn how to create a histogram to visualize data 
author: cawrites
ms.author: chadam
ms.date: 03/30/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: 
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# This article will describe how to use a histogram along with Python to visualize data. A histogram can be plotted to summarize distributed data using Mataplotlib. Mataplotlib can be used for visualizations to make bar charts, line plots, scatter plots, area charts.

## Install Python

Select the appropriate installation for Windows or Linux

Installation of [Python](https://www.python.org/downloads/)

## Install Matplotlib

Windows
```cmd
C:\python37\pip install matplotlib

Installing collected packages: pip
Successfully installed pip

```
Make sure the path to Python is part of the system path variable in Windows.

Linux Ubuntu

```bash
sudo apt-get install python3-matplotlib
```

 ## Collect data sample

 This data represents a sample of randomly generated numbers.

 ```cmd
1,1,2,4,4,5,6,6,9,10,
10,12,12,14,14,15,16,17,18,18,
18,19,20,20,20,22,22,22,24,24,
25,25,26,26,26,27,27,27,27,27
```

## Plot the data into a histogram

The data blocks contained in the sample will be placed in X number of bins to combine before getting the frequency.

We will use 10 to represent the number of bins.

```cmd
import matplotlib.pyplot as plt

x = [1,1,2,4,4,5,7,8,9,10,
     10,12,12,14,14,15,16,17,18,18,
     18,19,20,20,20,22,22,22,24,24,
     25,25,26,26,26,27,27,27,27,27,

      ]

plt.hist(x, bins=10)
plt.show()
```

Save test script as a file.  Run python script.

```cmd
C:\python37\python testscript.py
```
Histogram results using matplotlib and Python.
![Matplotlib Histogram](./media/python-histogram.png)


A histogram allows the user to discover the frequency by using continuous data. Data scientists can split the data into intervals, called bins. The larger the data sample, the more distribution of data is displayed in a histogram.