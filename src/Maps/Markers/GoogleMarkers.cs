﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Collections;

[assembly: WebResource("Velyo.Google.Maps.Markers.GoogleMarkersBehavior.js", "text/javascript")]
[assembly: WebResource("Velyo.Google.Maps.Markers.GoogleMarkersBehavior.min.js", "text/javascript")]

namespace Velyo.Google.Maps
{
    /// <summary>
    /// Extender control which targets GoogleMap control and adds the markers overlays to the google map.
    /// </summary>
    [PersistChildren(false)]
    [TargetControlType(typeof(GoogleMap))]
    [ToolboxData("<{0}:GoogleMarkers runat=server></{0}:GoogleMarkers>")]
    public class GoogleMarkers : DataBoundControl, IExtenderControl, IPostBackEventHandler
    {
        private InfoWindowOptions _infoWindowOptions;
        private MarkerOptions _options;
        private List<Marker> _markers;
        private ScriptManager _scriptManager;
        private string _targetControlID;


        /// <summary>
        /// This event is fired when the marker icon was clicked.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker's animation property changes.")]
        public event EventHandler<MarkerEventArgs> AnimationChanged;

        /// <summary>
        /// This event is fired when the marker icon was clicked.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker icon was clicked.")]
        public event EventHandler<MarkerEventArgs> Click;

        /// <summary>
        /// This event is fired when the marker clickable property was changed.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker clickable property was changed.")]
        public event EventHandler<MarkerEventArgs> ClickableChanged;

        /// <summary>
        /// This event is fired when the marker's cursor property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker's cursor property changes.")]
        public event EventHandler<MarkerEventArgs> CursorChanged;

        /// <summary>
        /// This event is fired when the marker icon was double clicked.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker icon was double clicked.")]
        public event EventHandler<MarkerEventArgs> DoubleClick;

        /// <summary>
        /// This event is repeatedly fired while the user drags the marker.
        /// </summary>
        [Category("Google")]
        [Description("This event is repeatedly fired while the user drags the marker.")]
        public event EventHandler<MarkerEventArgs> Drag;

        /// <summary>
        /// This event is fired when the marker's draggable property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker's draggable property changes.")]
        public event EventHandler<MarkerEventArgs> DraggableChanged;

        /// <summary>
        /// This event is fired when the user stops dragging the marker.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the user stops dragging the marker.")]
        public event EventHandler<MarkerEventArgs> DragEnd;

        /// <summary>
        /// This event is fired when the user starts dragging the marker.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the user starts dragging the marker.")]
        public event EventHandler<MarkerEventArgs> DragStart;

        /// <summary>
        /// This event is fired when the marker's flat property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker's flat property changes.")]
        public event EventHandler<MarkerEventArgs> FlatChanged;

        /// <summary>
        /// This event is fired when the marker icon property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker icon property changes.")]
        public event EventHandler<MarkerEventArgs> IconChanged;

        /// <summary>
        /// This event is fired for a mousedown on the marker.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired for a mousedown on the marker.")]
        public event EventHandler<MarkerEventArgs> MouseDown;

        /// <summary>
        /// This event is fired when the mouse leaves the area of the marker icon.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the mouse leaves the area of the marker icon.")]
        public event EventHandler<MarkerEventArgs> MouseOut;

        /// <summary>
        /// This event is fired when the mouse enters the area of the marker icon.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the mouse enters the area of the marker icon.")]
        public event EventHandler<MarkerEventArgs> MouseOver;

        /// <summary>
        /// This event is fired for a mouseup on the marker.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired for a mouseup on the marker.")]
        public event EventHandler<MarkerEventArgs> MouseUp;

        /// <summary>
        /// This event is fired when the marker position property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker position property changes.")]
        public event EventHandler<MarkerEventArgs> PositionChanged;

        /// <summary>
        /// This event is fired for a rightclick on the marker.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired for a rightclick on the marker.")]
        public event EventHandler<MarkerEventArgs> RightClick;

        /// <summary>
        /// This event is fired when the marker's shadow property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker's shadow property changes.")]
        public event EventHandler<MarkerEventArgs> ShadowChanged;

        /// <summary>
        /// This event is fired when the marker's shape property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker's shape property changes.")]
        public event EventHandler<MarkerEventArgs> ShapeChanged;

        /// <summary>
        /// This event is fired when the marker title property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker title property changes.")]
        public event EventHandler<MarkerEventArgs> TitleChanged;

        /// <summary>
        /// This event is fired when the marker's visible property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker's visible property changes.")]
        public event EventHandler<MarkerEventArgs> VisibleChanged;

        /// <summary>
        /// This event is fired when the marker's zIndex property changes.
        /// </summary>
        [Category("Google")]
        [Description("This event is fired when the marker's zIndex property changes.")]
        public event EventHandler<MarkerEventArgs> ZIndexChanged;


        /// <summary>
        /// Gets or sets the client animation changed handler.
        /// </summary>
        /// <value>The client animation changed handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client animation changed handler.")]
        public string OnClientAnimationChanged { get; set; }

        /// <summary>
        /// Gets or sets the client click handler.
        /// </summary>
        /// <value>The client click handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client click handler.")]
        public string OnClientClick { get; set; }

        /// <summary>
        /// Gets or sets the client clickable property changed handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client clickable property changed handler.")]
        public string OnClientClickableChanged { get; set; }

        /// <summary>
        /// Gets or sets the client cursor property changed handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client cursor property changed handler.")]
        public string OnClientCursorChanged { get; set; }

        /// <summary>
        /// Gets or sets the client double click handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client double click handler.")]
        public string OnClientDoubleClick { get; set; }

        /// <summary>
        /// Gets or sets the client drag handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client drag handler.")]
        public string OnClientDrag { get; set; }

        /// <summary>
        /// Gets or sets the client draggable property changed handler.
        /// </summary>
        /// <value>The client hanlder.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client draggable property changed handler.")]
        public string OnClientDraggableChanged { get; set; }

        /// <summary>
        /// Gets or sets the client drag end handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client drag end handler.")]
        public string OnClientDragEnd { get; set; }

        /// <summary>
        /// Gets or sets the client drag start handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client drag start handler.")]
        public string OnClientDragStart { get; set; }

        /// <summary>
        /// Gets or sets the client flat property changed handler.
        /// </summary>
        /// <value>The on client flat property changed handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client flat property changed handler.")]
        public string OnClientFlatChanged { get; set; }

        /// <summary>
        /// Gets or sets the client icon property changed handler.
        /// </summary>
        /// <value>The client icon property changed handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client icon property changed handler.")]
        public string OnClientIconChanged { get; set; }

        /// <summary>
        /// Gets or sets the client mouse down handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client-side mouse down handler.")]
        public string OnClientMouseDown { get; set; }

        /// <summary>
        /// Gets or sets the client mouse out handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client mouse out handler.")]
        public string OnClientMouseOut { get; set; }

        /// <summary>
        /// Gets or sets the client mouse over handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client mouse over handler.")]
        public string OnClientMouseOver { get; set; }

        /// <summary>
        /// Gets or sets the client mouse up handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client mouse up handler.")]
        public string OnClientMouseUp { get; set; }

        /// <summary>
        /// Gets or sets the client marker position property changed handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client marker position property changed handler.")]
        public string OnClientPositionChanged { get; set; }

        /// <summary>
        /// Gets or sets the client right click handler.
        /// </summary>
        /// <value>The client right click handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client right click handler.")]
        public string OnClientRightClick { get; set; }

        /// <summary>
        /// Gets or sets the client marker's shadow property changed handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client marker's shadow property changed handler.")]
        public string OnClientShadowChanged { get; set; }

        /// <summary>
        /// Gets or sets the client marker's shape property changed handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client marker's shape property changed handler.")]
        public string OnClientShapeChanged { get; set; }

        /// <summary>
        /// Gets or sets the client marker's title property changed handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client marker's title property changed handler.")]
        public string OnClientTitleChanged { get; set; }

        /// <summary>
        /// Gets or sets the client marker's visible property changed handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client marker's visible property changed handler.")]
        public string OnClientVisibleChanged { get; set; }

        /// <summary>
        /// Gets or sets the client marker's zIndex property changed handler.
        /// </summary>
        /// <value>The client handler.</value>
        [Category("Client Event")]
        [Description("Gets or sets the client marker's zIndex property changed handler.")]
        public string OnClientZIndexChanged { get; set; }

        /// <summary>
        /// Gets or sets the data address field.
        /// </summary>
        /// <value>The data address field.</value>
        [Category("Data")]
        public string DataAddressField { get; set; }

        /// <summary>
        /// Gets or sets the data icon field.
        /// </summary>
        /// <value>The data icon field.</value>
        [Category("Data")]
        public string DataIconField { get; set; }

        /// <summary>
        /// Gets or sets the data info field.
        /// </summary>
        /// <value>The data info field.</value>
        [Category("Data")]
        public string DataInfoField { get; set; }

        /// <summary>
        /// Gets or sets the data latitude field.
        /// </summary>
        /// <value>The data latitude field.</value>
        [Category("Data")]
        public string DataLatitudeField { get; set; }

        /// <summary>
        /// Gets or sets the data longitude field.
        /// </summary>
        /// <value>The data longitude field.</value>
        [Category("Data")]
        public string DataLongitudeField { get; set; }

        /// <summary>
        /// Options for the markers' info wondows. All markers' info windows will use these options.
        /// </summary>
        /// <value>The info window options.</value>
        [Category("Appearance")]
        [Description("Options for the markers' info wondows. All markers' info windows will use these options.")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public InfoWindowOptions InfoWindowOptions
        {
            get { return _infoWindowOptions ?? (_infoWindowOptions = new InfoWindowOptions()); }
            set { _infoWindowOptions = value; }
        }

        /// <summary>
        /// Options for the markers. All markers rendered will use these options.
        /// </summary>
        /// <value>The marker options.</value>
        [Category("Appearance")]
        [Description("Options for the markers. All markers rendered will use these options.")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public MarkerOptions MarkerOptions
        {
            get { return _options ?? (_options = new MarkerOptions()); }
            set { _options = value; }
        }

        /// <summary>
        /// Markers to be rendered to target map control.
        /// </summary>
        /// <value>The markers.</value>
        [Category("Appearance")]
        [Description("Markers to be rendered to target map control.")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Marker> Markers
        {
            get { return _markers ?? (_markers = new List<Marker>()); }
            set { _markers = value; }
        }

        /// <summary>
        /// Gets the script manager.
        /// </summary>
        /// <value>The script manager.</value>
        private ScriptManager ScriptManager
        {
            get
            {
                if (_scriptManager == null)
                {
                    var page = Page;
                    if (page != null)
                    {
                        _scriptManager = System.Web.UI.ScriptManager.GetCurrent(Page);
                        if (_scriptManager == null)
                            throw new InvalidOperationException(string.Format(
                                "The control with ID '{0}' requires a ScriptManager on the page. The ScriptManager must appear before any controls that need it.", ID));
                    }
                    else
                        throw new InvalidOperationException("Page cannot be null.");
                }
                return _scriptManager;
            }
        }

        /// <summary>
        /// Gets or sets the target control ID.
        /// </summary>
        /// <value>The target control ID.</value>
        [Category("Behavior")]
        [DefaultValue("")]
        [Description("Identifies the control to extend.")]
        [IDReferenceProperty]
        public string TargetControlID
        {
            get { return _targetControlID ?? string.Empty; }
            set { _targetControlID = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether a server control is rendered as UI on the page.
        /// </summary>
        /// <value></value>
        /// <returns>true if the control is visible on the page; otherwise false.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Visible
        {
            get { return base.Visible; }
            set { throw new NotSupportedException(); }
        }


        /// <summary>
        /// Finds the update panel.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        static UpdatePanel FindUpdatePanel(Control control)
        {
            Control parent = control.Parent;
            do
            {
                if (parent is UpdatePanel) return (parent as UpdatePanel);
            }
            while ((parent = parent.Parent) != null);
            return null;
        }

        /// <summary>
        /// Handles the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            RegisterWithScriptManager();
        }

        /// <summary>
        /// Registers the with script manager.
        /// </summary>
        private void RegisterWithScriptManager()
        {
            if (!string.IsNullOrEmpty(TargetControlID))
            {
                var target = FindControl(TargetControlID);
                if (target != null)
                {
                    if (FindUpdatePanel(this) == FindUpdatePanel(target))
                    {
                        ScriptManager.RegisterExtenderControl<GoogleMarkers>(this, target);
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            "An extender can't be in a different UpdatePanel than the control it extends.");
                    }
                }
                else
                {
                    throw new InvalidOperationException(string.Format(
                        "The TargetControlID of '{0}' is not valid. A control with ID '{1}' could not be found.", ID, TargetControlID));
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(
                    "The TargetControlID of '{0}' is not valid. The value cannot be null or empty.", ID));
            }
        }

        /// <summary>
        /// Renders the control to the specified HTML writer.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            if (!DesignMode)
            {
                ScriptManager.RegisterScriptDescriptors(this);
            }
        }

        #region IExtenderControl Members

        /// <summary>
        /// Registers the <see cref="T:System.Web.UI.ScriptDescriptor"/> objects for the control and returns an object that contains the <see cref="T:System.Web.UI.ScriptDescriptor"/> objects for the control.
        /// </summary>
        /// <param name="targetControl">The server control that the extender is associated with.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection that contains <see cref="T:System.Web.UI.ScriptDescriptor"/> objects.
        /// </returns>
        IEnumerable<ScriptDescriptor> IExtenderControl.GetScriptDescriptors(Control targetControl)
        {
            var descriptor = new ScriptBehaviorDescriptor("Velyo.Google.Maps.MarkersBehavior", targetControl.ClientID);

            // properties
            descriptor.AddProperty("name", UniqueID);
            if (_options != null)
            {
                descriptor.AddProperty("groupOptions", _options.ToScriptData());
            }
            if (_markers != null)
            {
                descriptor.AddProperty("markerOptions", _markers.Select(m => m.ToScriptData()).ToArray());
            }
            if (_infoWindowOptions != null)
            {
                descriptor.AddProperty("infoOptions", _infoWindowOptions.ToScriptData());
            }

            // events
            if (AnimationChanged != null)
            {
                descriptor.AddEvent("animationChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerAnimationChanged");
            }
            else if (OnClientAnimationChanged != null)
            {
                descriptor.AddEvent("animationChanged", OnClientAnimationChanged);
            }

            if (Click != null)
            {
                descriptor.AddEvent("click", "Velyo.Google.Maps.MarkersBehavior.raiseServerClick");
            }
            else if (OnClientClick != null)
            {
                descriptor.AddEvent("click", OnClientClick);
            }

            if (ClickableChanged != null)
            {
                descriptor.AddEvent("clickableChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerClickableChanged");
            }
            else if (OnClientClickableChanged != null)
            {
                descriptor.AddEvent("clickableChanged", OnClientClickableChanged);
            }

            if (CursorChanged != null)
            {
                descriptor.AddEvent("cursorChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerCursorChanged");
            }
            else if (OnClientCursorChanged != null)
            {
                descriptor.AddEvent("cursorChanged", OnClientCursorChanged);
            }

            if (DoubleClick != null)
            {
                descriptor.AddEvent("doubleClick", "Velyo.Google.Maps.MarkersBehavior.raiseServerDoubleClick");
            }
            else if (OnClientDoubleClick != null)
            {
                descriptor.AddEvent("doubleClick", OnClientDoubleClick);
            }

            if (Drag != null)
            {
                descriptor.AddEvent("drag", "Velyo.Google.Maps.MarkersBehavior.raiseServerDrag");
            }
            else if (OnClientDrag != null)
            {
                descriptor.AddEvent("drag", OnClientDrag);
            }

            if (DraggableChanged != null)
            {
                descriptor.AddEvent("draggableChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerDraggableChanged");
            }
            else if (OnClientDraggableChanged != null)
            {
                descriptor.AddEvent("draggableChanged", OnClientDraggableChanged);
            }

            if (DragEnd != null)
            {
                descriptor.AddEvent("dragEnd", "Velyo.Google.Maps.MarkersBehavior.raiseServerDragEnd");
            }
            else if (OnClientDragEnd != null)
            {
                descriptor.AddEvent("dragEnd", OnClientDragEnd);
            }

            if (DragStart != null)
            {
                descriptor.AddEvent("dragStart", "Velyo.Google.Maps.MarkersBehavior.raiseServerDragStart");
            }
            else if (OnClientDragStart != null)
            {
                descriptor.AddEvent("dragStart", OnClientDragStart);
            }

            if (FlatChanged != null)
            {
                descriptor.AddEvent("flatChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerFlatChanged");
            }
            else if (OnClientFlatChanged != null)
            {
                descriptor.AddEvent("flatChanged", OnClientFlatChanged);
            }

            if (IconChanged != null)
            {
                descriptor.AddEvent("iconChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerIconChanged");
            }
            else if (OnClientIconChanged != null)
            {
                descriptor.AddEvent("iconChanged", OnClientIconChanged);
            }

            if (MouseDown != null)
            {
                descriptor.AddEvent("mouseDown", "Velyo.Google.Maps.MarkersBehavior.raiseServerMouseDown");
            }
            else if (OnClientMouseDown != null)
            {
                descriptor.AddEvent("mouseDown", OnClientMouseDown);
            }

            if (MouseOut != null)
            {
                descriptor.AddEvent("mouseOut", "Velyo.Google.Maps.MarkersBehavior.raiseServerMouseOut");
            }
            else if (OnClientMouseOut != null)
            {
                descriptor.AddEvent("mouseOut", OnClientMouseOut);
            }

            if (MouseOver != null)
            {
                descriptor.AddEvent("mouseOver", "Velyo.Google.Maps.MarkersBehavior.raiseServerMouseOver");
            }
            else if (OnClientMouseOver != null)
            {
                descriptor.AddEvent("mouseOver", OnClientMouseOver);
            }

            if (MouseUp != null)
            {
                descriptor.AddEvent("mouseUp", "Velyo.Google.Maps.MarkersBehavior.raiseServerMouseUp");
            }
            else if (OnClientMouseUp != null)
            {
                descriptor.AddEvent("mouseUp", OnClientMouseUp);
            }

            if (PositionChanged != null)
            {
                descriptor.AddEvent("positionChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerPositionChanged");
            }
            else if (OnClientPositionChanged != null)
            {
                descriptor.AddEvent("positionChanged", OnClientPositionChanged);
            }

            if (RightClick != null)
            {
                descriptor.AddEvent("rightClick", "Velyo.Google.Maps.MarkersBehavior.raiseServerRightClick");
            }
            else if (OnClientRightClick != null)
            {
                descriptor.AddEvent("rightClick", OnClientRightClick);
            }

            if (ShadowChanged != null)
            {
                descriptor.AddEvent("shadowChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerShadowChanged");
            }
            else if (OnClientShadowChanged != null)
            {
                descriptor.AddEvent("shadowChanged", OnClientShadowChanged);
            }

            if (ShapeChanged != null)
            {
                descriptor.AddEvent("shapeChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerShapeChanged");
            }
            else if (OnClientShapeChanged != null)
            {
                descriptor.AddEvent("shapeChanged", OnClientShapeChanged);
            }

            if (TitleChanged != null)
            {
                descriptor.AddEvent("titleChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerTitleChanged");
            }
            else if (OnClientTitleChanged != null)
            {
                descriptor.AddEvent("titleChanged", OnClientTitleChanged);
            }

            if (VisibleChanged != null)
            {
                descriptor.AddEvent("visibleChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerVisibleChanged");
            }
            else if (OnClientVisibleChanged != null)
            {
                descriptor.AddEvent("visibleChanged", OnClientVisibleChanged);
            }

            if (ZIndexChanged != null)
            {
                descriptor.AddEvent("zindexChanged", "Velyo.Google.Maps.MarkersBehavior.raiseServerZIndexChanged");
            }
            else if (OnClientZIndexChanged != null)
            {
                descriptor.AddEvent("zindexChanged", OnClientZIndexChanged);
            }

            yield return descriptor;
        }

        /// <summary>
        /// Registers the script libraries for the control and returns an enumeration of ECMAScript (JavaScript) files that have been registered as embedded resources.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerable"/> collection that contains JavaScript files that have been registered as embedded resources.
        /// </returns>
        IEnumerable<ScriptReference> IExtenderControl.GetScriptReferences()
        {
            string assembly = GetType().Assembly.FullName;
#if DEBUG
            yield return new ScriptReference("Velyo.Google.Maps.Markers.GoogleMarkersBehavior.js", assembly);
#else
            yield return new ScriptReference("Velyo.Google.Maps.Markers.GoogleMarkersBehavior.min.js", assembly);
#endif
        }
        #endregion

        #region Event Methods

        /// <summary>
        /// Raises the <see cref="E:AnimationChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnAnimationChanged(MarkerEventArgs e)
        {
            AnimationChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:Click"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnClick(MarkerEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ClickableChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnClickableChanged(MarkerEventArgs e)
        {
            ClickableChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:CursorChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCursorChanged(MarkerEventArgs e)
        {
            CursorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:DoubleClick"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDoubleClick(MarkerEventArgs e)
        {
            DoubleClick?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:Drag"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDrag(MarkerEventArgs e)
        {
            Drag?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:DraggableChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDraggableChanged(MarkerEventArgs e)
        {
            DraggableChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:DragEnd"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDragEnd(MarkerEventArgs e)
        {
            DragEnd?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:DragStart"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDragStart(MarkerEventArgs e)
        {
            DragStart?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:FlatChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnFlatChanged(MarkerEventArgs e)
        {
            FlatChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:IconChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnIconChanged(MarkerEventArgs e)
        {
            IconChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseDown"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseDown(MarkerEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseOut"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseOut(MarkerEventArgs e)
        {
            MouseOut?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseOver"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseOver(MarkerEventArgs e)
        {
            MouseOver?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseUp"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMouseUp(MarkerEventArgs e)
        {
            MouseUp?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:PositionChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPositionChanged(MarkerEventArgs e)
        {
            PositionChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:RightClick"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected virtual void OnRightClick(MarkerEventArgs e)
        {
            RightClick?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ShadowChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnShadowChanged(MarkerEventArgs e)
        {
            ShadowChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ShapeChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnShapeChanged(MarkerEventArgs e)
        {
            ShapeChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:TitleChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnTitleChanged(MarkerEventArgs e)
        {
            TitleChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:VisibleChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnVisibleChanged(MarkerEventArgs e)
        {
            VisibleChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ZIndexChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MarkerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnZIndexChanged(MarkerEventArgs e)
        {
            ZIndexChanged?.Invoke(this, e);
        }

        /// <summary>
        /// When implemented by a class, enables a server control to process an event raised when a form is posted to the server.
        /// </summary>
        /// <param name="eventArgument">A <see cref="T:System.String"/> that represents an optional event argument to be passed to the event handler.</param>
        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            var ser = new JavaScriptSerializer();
            dynamic args = ser.DeserializeObject(eventArgument);
            if (args != null)
            {
                string name = args["name"];
                var e = MarkerEventArgs.FromScriptData(args);
                switch (name)
                {
                    case "animationChanged":
                        this.OnAnimationChanged(e);
                        break;
                    case "click":
                        this.OnClick(e);
                        break;
                    case "clickableChanged":
                        this.OnClickableChanged(e);
                        break;
                    case "cursorChanged":
                        this.OnCursorChanged(e);
                        break;
                    case "doubleClick":
                        this.OnDoubleClick(e);
                        break;
                    case "drag":
                        this.OnDrag(e);
                        break;
                    case "draggableChanged":
                        this.OnDraggableChanged(e);
                        break;
                    case "dragEnd":
                        this.OnDragEnd(e);
                        break;
                    case "dragStart":
                        this.OnDragStart(e);
                        break;
                    case "flatChanged":
                        this.OnFlatChanged(e);
                        break;
                    case "iconChanged":
                        this.OnIconChanged(e);
                        break;
                    case "mouseDown":
                        this.OnMouseDown(e);
                        break;
                    case "mouseOut":
                        this.OnMouseOut(e);
                        break;
                    case "mouseOver":
                        this.OnMouseOver(e);
                        break;
                    case "mouseUp":
                        this.OnMouseUp(e);
                        break;
                    case "positionChanged":
                        this.OnPositionChanged(e);
                        break;
                    case "rightClick":
                        this.OnRightClick(e);
                        break;
                    case "shadowChanged":
                        this.OnShadowChanged(e);
                        break;
                    case "shapeChanged":
                        this.OnShapeChanged(e);
                        break;
                    case "titleChanged":
                        this.OnTitleChanged(e);
                        break;
                    case "visibleChanged":
                        this.OnVisibleChanged(e);
                        break;
                    case "zindexChanged":
                        this.OnZIndexChanged(e);
                        break;
                }
            }
        }
        #endregion

        #region DataBound Methods

        /// <summary>
        /// When overridden in a derived class, binds data from the data source to the control.
        /// </summary>
        /// <param name="data">The <see cref="T:System.Collections.IEnumerable"/> list of data returned from a <see cref="M:System.Web.UI.WebControls.DataBoundControl.PerformSelect"/> method call.</param>
        protected override void PerformDataBinding(IEnumerable data)
        {
            base.PerformDataBinding(data);

            if (data != null)
            {
                bool hasAddressDataField = !string.IsNullOrEmpty(DataAddressField);
                bool hasIconDataField = !string.IsNullOrEmpty(DataIconField);
                bool hasInfoDataField = !string.IsNullOrEmpty(DataInfoField);
                bool hasLatitudeDataField = !string.IsNullOrEmpty(DataLatitudeField);
                bool hasLongitudeDataField = !string.IsNullOrEmpty(DataLongitudeField);

                Marker marker;
                foreach (object dataItem in data)
                {
                    marker = new Marker();

                    //if (hasAddressDataField)
                    //    marker.Address = DataBinder.Eval(dataItem, DataAddressField, "");
                    if (hasIconDataField)
                        marker.Icon = DataBinder.Eval(dataItem, DataIconField, null);
                    if (hasInfoDataField)
                        marker.Info = DataBinder.Eval(dataItem, DataInfoField, null);
                    if (hasLatitudeDataField)
                        marker.Position.Latitude = (double)DataBinder.Eval(dataItem, DataLatitudeField);
                    if (hasLongitudeDataField)
                        marker.Position.Longitude = (double)DataBinder.Eval(dataItem, DataLongitudeField);

                    Markers.Add(marker);
                }
            }
        }

        /// <summary>
        /// Retrieves data from the associated data source.
        /// </summary>
        protected override void PerformSelect()
        {
            // Call OnDataBinding here if bound to a data source using the
            // DataSource property (instead of a DataSourceID), because the
            // databinding statement is evaluated before the call to GetData.       
            if (!IsBoundUsingDataSourceID) OnDataBinding(EventArgs.Empty);

            // The GetData method retrieves the DataSourceView object from  
            // the IDataSource associated with the data-bound control.            
            GetData().Select(
                CreateDataSourceSelectArguments(),
                (data) =>
                {
                    // Call OnDataBinding only if it has not already been 
                    // called in the PerformSelect method.
                    if (IsBoundUsingDataSourceID) OnDataBinding(EventArgs.Empty);
                    // The PerformDataBinding method binds the data in the  
                    // retrievedData collection to elements of the data-bound control.
                    PerformDataBinding(data);
                });

            // The PerformDataBinding method has completed.
            RequiresDataBinding = false;
            MarkAsDataBound();

            // Raise the DataBound event.
            OnDataBound(EventArgs.Empty);
        }
        #endregion
    }
}
