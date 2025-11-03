# XML Importer Tool

A lightweight **WPF application** designed to automatically import XML data into SQL Server.  
It intelligently detects XML structures and maps them to database tables — no manual configuration required.  
The tool’s connection layer is modular, allowing easy extension for other database engines such as **MySQL**, **PostgreSQL**, or **SQLite** in future versions.

## ✨ Features
- 🧠 **Automatic XML structure detection**
- 🗂️ **Dynamic table mapping** based on XML schema
- ⚙️ **Flexible connection layer** — easily add new DB providers
- 🧾 **Command-line interface (CLI) support** via markdown-based configuration
- 🖥️ **Modern WPF UI** with simple workflow for import preview and execution
- 💾 **Save and load connection profiles**

## 🚀 Future Extensions
- Support for **MySQL** and **PostgreSQL**
- Advanced XML validation and schema mapping
- Batch import and data transformation

## 🧱 Tech Stack
- **.NET Framework 4.6 / C# 6**
- **WPF** (MVVM)
- **ADO.NET / Dapper**
- **XML LINQ (XDocument)**
- **Markdown CLI instruction generator**

## 📄 Example CLI Instruction
```md
# XML Import Task
source: "data/orders.xml"
target: "SQLServer"
connection: "Server=.;Database=ImportDB;Trusted_Connection=True;"
table: "Orders"
mode: "InsertOrUpdate"
