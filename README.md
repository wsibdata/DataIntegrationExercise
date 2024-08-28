# Data Integration Exercise

## Washington State Investment Board
### Investment Data Engineer Technical Interview 

---

A. Scenario Overview
We have acquired a new data set and need to onboard it. The data consists of cash transactions to funds over time. We have a file for each fund and will need to build a process to load them to a table. From what we can tell the data looks pretty good in terms of consistency and formatting, but we can't be certain. We also have a reference file that tells us a category type for each cash flow transaction (whether it is a capital call or distribution). Once the data is stored and verified, we will need to build reports for our users.
Your task will consist of four primary phases:
1. Programming: Build a process to ingest the data files and store them to a database table
1. Validation: Validate that the data is clean and does not have major errors
1. Data Prep & Reporting: Manipulate the data in report ready format and build a report

Please note, the objective of this exercise is to understand how candidates think about and approach real data problems. It is not a graded exercise of ability to write code. Time is limited, so you find yourself stuck on a section try to move on and we can discuss as a group at the end of the exercise.
_It is possible to be successful without fully completing all steps of the exercise._

---

B. Programming: Build a Data Load Process
The data we are looking to load sits in 596 CSV files located in the Fund Data folder. Each file is labeled with a fund code (e.g. 00E-ZYZ7U) and will contain historical cash flows for that fund. File structures should be similar across the files. These files should all be loaded into a single SQL table.
We will also need to load data from the *TransactionMap* excel file. This file contains a mapping of transaction types that will be needed later.
Task: Use C# or Python to develop a process for loading the data in these files into database tables. You will need to design the database tables and load all relevant data.

---

C. Validation: Check Data Quality

Now that the data has been loaded to the database, we will need to check if it loaded successfully. We'll want to check record counts and whether there are any NULL or invalid records. Certain source files were created manually, so it is possible there might be some data entry errors as well. 
Task: Develop a process to check: 
1. record count
1. fund count
1. NULL records, 
1. invalid data types (e.g. text in a date field). 

Consider any other types of data checks we might want to think about as well.

---
D. Data Prep & Reporting

The next step after data validation is transforming the data and produce reports on various metrics. To produce the required reporting outputs, we need to map in *Transaction Categories* from the *TransactionMap file*. Transaction categories tell us if the fund received incoming money (Call) or if it paid-out money (distribution). 

The final step is connecting a reporting tool such as Power BI to our data and generating some reporting visuals.

Task: Join the data from the TransactionMap to the fund Transaction Data (loaded from the 596 files). Create the following SQL views to report: 
1. ONLY Calls and Distributions (no Market Values or Commitments)
1. ONLY Market Values (no Calls, Distributions, or Commitments)
1. Create slicers that allow a user to filter the tables by Fund and Date

---

*BONUS*: 
If time allows, please write SQL queries to report:
1. Aggregate Calls and Distributions by quarter using Quarter End Dates (12/31, 3/31, 6/30, 9/30)
1. The last Market Value by fund on each Quarter End Date. 


