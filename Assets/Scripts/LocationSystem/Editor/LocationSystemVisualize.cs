using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Prechool.LocationSystem
{
    public class LocationSystemViewer : EditorWindow
    {
        [SerializeField] private int m_SelectedIndex = -1;
        [SerializeField] private float m_MapScale = 0.5f;
        [SerializeField] private float m_LocScale = 0.4f;
        [SerializeField] private MapMeta mapMeta;
        private Image mapElement;
        private ListView listView;

        [MenuItem("Preschool/Location System/Visualize")]
        public static void ShowMyEditor()
        {
            // This method is called when the user selects the menu item in the Editor.
            EditorWindow wnd = GetWindow<LocationSystemViewer>();
            wnd.titleContent = new GUIContent("AR Location Event Viewer");

            // Limit size of the window.
            wnd.minSize = new Vector2(450, 200);
        }

        public void CreateGUI()
        {
            var holder = MyEditorUtils.FindFirstAsset<LocationEventHolder>();
            var allObjects = holder.locationEvents;

            // Create a two-pane view with the left pane being fixed.
            var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

            // Add the panel to the visual tree by adding it as a child to the root element.
            rootVisualElement.Add(splitView);

            var rightPane = new VisualElement();
            rightPane.Add(MapOptions());

            // Map Holder
            var mapHolder = new ScrollView();
            mapHolder.style.flexGrow = 1;
            mapHolder.mode = ScrollViewMode.VerticalAndHorizontal;
            
            rightPane.Add(mapHolder);

            mapElement = new Image();
            mapElement.image = mapMeta.forestMap;
            mapElement.style.width = mapMeta.forestMap.width * m_MapScale;
            mapElement.style.height = mapMeta.forestMap.height * m_MapScale;
            mapHolder.Add(mapElement);

            // Left Pane
            var leftPane = new ListView();
            leftPane.selectionType = SelectionType.Multiple;
            leftPane.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            leftPane.makeItem = () => new Label();
            leftPane.bindItem = (item, index) =>
            {
                LocationEvent locEvt = allObjects[index];
                (item as Label).text = $"{locEvt.name}\nGPS: {locEvt.location.latiude}, {locEvt.location.longitude}";

            };

            leftPane.itemsSource = allObjects;
            leftPane.selectionChanged += OnSelectedChange;
            leftPane.selectedIndex = m_SelectedIndex;
            leftPane.selectionChanged += (items) => { m_SelectedIndex = leftPane.selectedIndex; };

            splitView.Add(leftPane);
            splitView.Add(rightPane);

            listView = leftPane;
        }

        private void OnSelectedChange(IEnumerable<object> selectedItems)
        {
            mapElement.Clear();
            var enumerator = selectedItems.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var locEvent = enumerator.Current as LocationEvent;
                if (locEvent != null)
                {
                    var percentage = GetMapPercentage(locEvent.location);
                    var symbol = new Image();
                    symbol.image = mapMeta.locSymbol;
                    symbol.style.width = mapMeta.locSymbol.width * m_LocScale;
                    symbol.style.height = mapMeta.locSymbol.height * m_LocScale;
                    symbol.style.position = Position.Absolute;
                    symbol.style.left = percentage.Item1 * mapElement.style.width.value.value;
                    symbol.style.top = percentage.Item2 * mapElement.style.height.value.value;
                    symbol.style.translate = new Translate(-mapMeta.locSymbol.width / 2 * m_LocScale, -mapMeta.locSymbol.height * m_LocScale, 0);

                    var circle = new VisualElement();
                    circle.style.width = locEvent.radiusM * 2 / mapMeta.MapWidthMetres * mapElement.style.width.value.value;
                    circle.style.height = locEvent.radiusM * 2 / mapMeta.MapHeightMetres * mapElement.style.height.value.value;
                    circle.style.left = percentage.Item1 * mapElement.style.width.value.value;
                    circle.style.top = percentage.Item2 * mapElement.style.height.value.value;
                    circle.style.backgroundColor = new Color(0, 0.5f, 1, 0.8f);
                    circle.SetBorderRadius(new Length(100, LengthUnit.Percent));
                    circle.style.translate = new Translate(-circle.style.width.value.value / 2, -circle.style.width.value.value / 2, 0);

                    mapElement.Add(circle);
                    mapElement.Add(symbol);
                }
            }
        }

        private Tuple<float, float> GetMapPercentage(GPSCoord coord)
        {
            var x = (coord.longitude - mapMeta.topLeftCoord.longitude) / (mapMeta.bottomRightCoord.longitude - mapMeta.topLeftCoord.longitude);
            var y = (coord.latiude - mapMeta.topLeftCoord.latiude) / (mapMeta.bottomRightCoord.latiude - mapMeta.topLeftCoord.latiude);
            return new Tuple<float, float>(x, y);
        }

        private VisualElement MapOptions()
        {
            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;
            container.style.justifyContent = Justify.Center;

            var mapScale = new Slider("Map Scale", 0.2f, 2, SliderDirection.Horizontal);
            var locScale = new Slider("Loc Symbol Scale", 0, 0.5f, SliderDirection.Horizontal);

            mapScale.style.flexGrow = 1;
            locScale.style.flexGrow = 1;

            mapScale.value = m_MapScale;
            locScale.value = m_LocScale;
            mapScale.RegisterValueChangedCallback((evt) =>
            {
                m_MapScale = evt.newValue;
                mapElement.style.width = mapMeta.forestMap.width * m_MapScale;
                mapElement.style.height = mapMeta.forestMap.height * m_MapScale;
                OnSelectedChange(listView.selectedItems);

            });
            locScale.RegisterValueChangedCallback((evt) =>
            {
                m_LocScale = evt.newValue;
                foreach (var loc in mapElement.Children())
                {
                    if (loc is not Image)
                    {
                        continue;
                    }
                    loc.style.width = mapMeta.locSymbol.width * m_LocScale;
                    loc.style.height = mapMeta.locSymbol.height * m_LocScale;
                    loc.style.translate = new Translate(-mapMeta.locSymbol.width / 2 * m_LocScale, -mapMeta.locSymbol.height * m_LocScale, 0);
                }
            });

            var refreshBtn = new Button();
            refreshBtn.text = "Refresh";
            refreshBtn.clicked += () => { rootVisualElement.Clear(); CreateGUI(); };

            container.Add(refreshBtn);
            container.Add(mapScale);
            container.Add(locScale);

            return container;
        }
    }
}