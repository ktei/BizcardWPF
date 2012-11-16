using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace LiteApp.Bizcard.ViewModels
{
    public class MessageViewModel : Screen
    {
        Buttons _buttons;
        HeaderIcon _headerIcon;

        public MessageViewModel(string header, string contents, Buttons buttons = Buttons.OK, HeaderIcon icon = HeaderIcon.Exclamation)
        {
            _header = header;
            _contents = contents;
            _buttons = buttons;
            _headerIcon = icon;
            DisplayName = _header;
        }

        string _header;
        string _contents;

        public string Header
        {
            get { return _header; }
            set
            {
                if (_header != value)
                {
                    _header = value;
                    NotifyOfPropertyChange(() => Header);
                }
            }
        }

        public string Contents
        {
            get { return _contents; }
            set
            {
                if (_contents != value)
                {
                    _contents = value;
                    NotifyOfPropertyChange(() => Contents);
                }
            }
        }

        public Buttons ButtonOptions
        {
            get { return _buttons; }
            set
            {
                if (_buttons != value)
                {
                    _buttons = value;
                    NotifyOfPropertyChange(() => this.ButtonOptions);
                }
            }
        }

        public HeaderIcon Icon
        {
            get { return _headerIcon; }
            set
            {
                if (_headerIcon != value)
                {
                    _headerIcon = value;
                    NotifyOfPropertyChange(() => this.Icon);
                }
            }
        }

        public enum Buttons
        {
            OK = 0,
            OKCancel,
            YesNo
        }

        public enum HeaderIcon
        {
            Exclamation = 0,
            Question
        }
    }
}
