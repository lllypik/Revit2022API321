using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022API324
{
    [Transaction(TransactionMode.Manual)]

    public class Main : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;

            ElementId firstFloor = new ElementId(378117);
            ElementId secondFloor = new ElementId(378118);

            ElementLevelFilter elementFirstFloorFilter = new ElementLevelFilter(firstFloor);
            ElementLevelFilter elementSecondFloorFilter = new ElementLevelFilter(secondFloor);

            ElementClassFilter wallsInstsncesFilter = new ElementClassFilter(typeof(Wall));

            LogicalAndFilter wallsFirstFloorFilter = new LogicalAndFilter(wallsInstsncesFilter, elementFirstFloorFilter);
            LogicalAndFilter wallsSecondFloorFilter = new LogicalAndFilter(wallsInstsncesFilter, elementSecondFloorFilter);


            var wallsFirstFloor = new FilteredElementCollector(document)
                .WherePasses(wallsFirstFloorFilter)
                .Cast<Wall>()
                .ToList();

            var wallsSecondFloor = new FilteredElementCollector(document)
                .WherePasses(wallsSecondFloorFilter)
                .Cast<Wall>()
                .ToList();

            TaskDialog.Show
                ("Walls count",
                $"Количество стен на первом этаже {wallsFirstFloor.Count}{Environment.NewLine}Количество стен на втором этаже {wallsSecondFloor.Count}");

            return Result.Succeeded;

        }
    }
}
