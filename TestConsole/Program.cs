using ParamAccessHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GetTestCaseParams p = new GetTestCaseParams();

            //Need to put this in app.config
            //possibly need to restrict this from GIT
            //Or Use Environment Variables
            String testcaseID = "-YourTestCaseID-";
            string query = "{ \"query\": \"Select [System.Id] From WorkItems Where [System.WorkItemType] = 'Test Case' AND [State] <> 'Closed' AND [State] <> 'Removed' AND [Microsoft.VSTS.TCM.AutomatedTestName] = 'SimpleAppAppiumTests.SimpleAppTestScenarios.AddUITest'\"}";
            p.Pat = "-YourPAT-";
            p.VstsURI = "https://-YourInstance-.visualstudio.com";

            DataSet paramDs = new DataSet();
            Task.Run(async () => { paramDs = await p.GetParams(testcaseID); }).GetAwaiter().GetResult();
             
            Console.WriteLine(p.TestCaseJASON);
            Console.WriteLine();

            DisplayParams(paramDs);

            Console.ReadLine();

            Console.WriteLine();

            List<string> ids = new List<string>();
            Task.Run(async () => { ids = await p.GetTestCasesByQuery(query); }).GetAwaiter().GetResult();

            foreach(string id in ids)
            {
                Console.WriteLine(id);
                Task.Run(async () => { paramDs = await p.GetParams(id); }).GetAwaiter().GetResult();
                DisplayParams(paramDs);
            }

            Console.ReadLine();
        }

        static void DisplayParams(DataSet ds)
        {
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                Console.Write(dc.ColumnName);
                Console.Write("\t");
            }

            Console.WriteLine();
            Console.WriteLine("-------------------");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
        }
    }
}
