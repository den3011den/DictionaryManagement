using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
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
            base.ColumnsText = "Не выбрано";
            base.PagingSummaryFormat = $"Страница {{0}} из {{1}} (всего записей {{2}} )";
            //base.EmptyText = "Нет записей для отображения";
            base.EmptyTemplate = BuildRenderTree;
        }

        public void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(1, "p");
            builder.AddAttribute(2, "style", "color: lightgrey; font-size: 24px; text-align: center; margin: 2rem;");
            builder.AddContent(3, "Нет записей для отображения");
            builder.CloseElement();            
        }
    }
}
