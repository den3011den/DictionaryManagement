using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace DictionaryManagement_Server.Extensions
{
    public class RadzenDataGridApp<TItem> : RadzenDataGrid<TItem>
    {
        public RadzenDataGridApp() : base()
        {

            base.AndOperatorText = "И";
            base.OrOperatorText = "ИЛИ";
            base.StartsWithText = "Начинается с";
            base.ApplyFilterText = "Применить";
            base.ClearFilterText = "Очистить";
            base.ContainsText = "Содержит";
            base.EndsWithText = "Кончается на";
            base.DoesNotContainText = "Не содержит";
            base.EqualsText = "Равно";
            base.GreaterThanOrEqualsText = "Больше или равно";
            base.GreaterThanText = "Больше чем";
            base.IsEmptyText = "Пусто (пустая строка)";
            base.IsNotEmptyText = "Не пусто (не пустая строка)";
            base.LessThanOrEqualsText = "Меньше или равно";
            base.LessThanText = "Меньше чем";
            base.IsNotNullText = "Не пусто (не NULL)";
            base.IsNullText = "Пусто (NULL)";
            base.NotEqualsText = "Не равно";
            base.FilterText = "Фильтр";
            base.AllColumnsText = "Все";
            base.ColumnsShowingText = "колонок отображается";
        }
    }
}
