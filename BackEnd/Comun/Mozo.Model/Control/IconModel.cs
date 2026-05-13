using System;

using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;

namespace Mozo.Model.Control;

//public class IconSettingModel
//{
//    public string Html { get; set; }
//    public int Color { get; set; }
//    public int Size { get; set; }    
//}

public static class IconType
{
    public static class General
    {
        public const string Gen005 =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M19 22H5C4.4 22 4 21.6 4 21V3C4 2.4 4.4 2 5 2H14L20 8V21C20 21.6 19.6 22 19 22ZM12.5 18C12.5 17.4 12.6 17.5 12 17.5H8.5C7.9 17.5 8 17.4 8 18C8 18.6 7.9 18.5 8.5 18.5L12 18C12.6 18 12.5 18.6 12.5 18ZM16.5 13C16.5 12.4 16.6 12.5 16 12.5H8.5C7.9 12.5 8 12.4 8 13C8 13.6 7.9 13.5 8.5 13.5H15.5C16.1 13.5 16.5 13.6 16.5 13ZM12.5 8C12.5 7.4 12.6 7.5 12 7.5H8C7.4 7.5 7.5 7.4 7.5 8C7.5 8.6 7.4 8.5 8 8.5H12C12.6 8.5 12.5 8.6 12.5 8Z\" fill=\"currentColor\"/><rect x=\"7\" y=\"17\" width=\"6\" height=\"2\" rx=\"1\" fill=\"currentColor\"/><rect x=\"7\" y=\"12\" width=\"10\" height=\"2\" rx=\"1\" fill=\"currentColor\"/><rect x=\"7\" y=\"7\" width=\"6\" height=\"2\" rx=\"1\" fill=\"currentColor\"/><path d=\"M15 8H20L14 2V7C14 7.6 14.4 8 15 8Z\" fill=\"currentColor\"/></svg></span>";

        //Lupa
        public const string Gen021 =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><rect opacity=\"0.5\" x=\"17.0365\" y=\"15.1223\" width=\"8.15546\" height=\"2\" rx=\"1\" transform=\"rotate(45 17.0365 15.1223)\" fill=\"currentColor\"/><path d=\"M11 19C6.55556 19 3 15.4444 3 11C3 6.55556 6.55556 3 11 3C15.4444 3 19 6.55556 19 11C19 15.4444 15.4444 19 11 19ZM11 5C7.53333 5 5 7.53333 5 11C5 14.4667 7.53333 17 11 17C14.4667 17 17 14.4667 17 11C17 7.53333 14.4667 5 11 5Z\" fill=\"currentColor\"/></svg></span>";

        public const string Gen025 =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><rect x=\"2\" y=\"2\" width=\"9\" height=\"9\" rx=\"2\" fill=\"currentColor\"/><rect opacity=\"0.3\" x=\"13\" y=\"2\" width=\"9\" height=\"9\" rx=\"2\" fill=\"currentColor\"/><rect opacity=\"0.3\" x=\"13\" y=\"13\" width=\"9\" height=\"9\" rx=\"2\" fill=\"currentColor\"/><rect opacity=\"0.3\" x=\"2\" y=\"13\" width=\"9\" height=\"9\" rx=\"2\" fill=\"currentColor\"/></svg></span>";

        //gen046.svg
        public const string Help =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><rect opacity=\"0.3\" x=\"2\" y=\"2\" width=\"20\" height=\"20\" rx=\"10\" fill=\"currentColor\"/><path d=\"M11.276 13.654C11.276 13.2713 11.3367 12.9447 11.458 12.674C11.5887 12.394 11.738 12.1653 11.906 11.988C12.0833 11.8107 12.3167 11.61 12.606 11.386C12.942 11.1247 13.1893 10.896 13.348 10.7C13.5067 10.4947 13.586 10.2427 13.586 9.944C13.586 9.636 13.4833 9.356 13.278 9.104C13.082 8.84267 12.69 8.712 12.102 8.712C11.486 8.712 11.066 8.866 10.842 9.174C10.6273 9.482 10.52 9.82267 10.52 10.196L10.534 10.574H8.826C8.78867 10.3967 8.77 10.2333 8.77 10.084C8.77 9.552 8.90067 9.07133 9.162 8.642C9.42333 8.20333 9.81067 7.858 10.324 7.606C10.8467 7.354 11.4813 7.228 12.228 7.228C13.1987 7.228 13.9687 7.44733 14.538 7.886C15.1073 8.31533 15.392 8.92667 15.392 9.72C15.392 10.168 15.322 10.5507 15.182 10.868C15.042 11.1853 14.874 11.442 14.678 11.638C14.482 11.834 14.2253 12.0533 13.908 12.296C13.544 12.576 13.2733 12.8233 13.096 13.038C12.928 13.2527 12.844 13.528 12.844 13.864V14.326H11.276V13.654ZM11.192 15.222H12.928V17H11.192V15.222Z\" fill=\"currentColor\"/></svg></span>";

        //gen014.svg
        public const string Calendar =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M21 22H3C2.4 22 2 21.6 2 21V5C2 4.4 2.4 4 3 4H21C21.6 4 22 4.4 22 5V21C22 21.6 21.6 22 21 22Z\" fill=\"currentColor\"/><path d=\"M6 6C5.4 6 5 5.6 5 5V3C5 2.4 5.4 2 6 2C6.6 2 7 2.4 7 3V5C7 5.6 6.6 6 6 6ZM11 5V3C11 2.4 10.6 2 10 2C9.4 2 9 2.4 9 3V5C9 5.6 9.4 6 10 6C10.6 6 11 5.6 11 5ZM15 5V3C15 2.4 14.6 2 14 2C13.4 2 13 2.4 13 3V5C13 5.6 13.4 6 14 6C14.6 6 15 5.6 15 5ZM19 5V3C19 2.4 18.6 2 18 2C17.4 2 17 2.4 17 3V5C17 5.6 17.4 6 18 6C18.6 6 19 5.6 19 5Z\" fill=\"currentColor\"/><path d=\"M8.8 13.1C9.2 13.1 9.5 13 9.7 12.8C9.9 12.6 10.1 12.3 10.1 11.9C10.1 11.6 10 11.3 9.8 11.1C9.6 10.9 9.3 10.8 9 10.8C8.8 10.8 8.59999 10.8 8.39999 10.9C8.19999 11 8.1 11.1 8 11.2C7.9 11.3 7.8 11.4 7.7 11.6C7.6 11.8 7.5 11.9 7.5 12.1C7.5 12.2 7.4 12.2 7.3 12.3C7.2 12.4 7.09999 12.4 6.89999 12.4C6.69999 12.4 6.6 12.3 6.5 12.2C6.4 12.1 6.3 11.9 6.3 11.7C6.3 11.5 6.4 11.3 6.5 11.1C6.6 10.9 6.8 10.7 7 10.5C7.2 10.3 7.49999 10.1 7.89999 10C8.29999 9.90003 8.60001 9.80003 9.10001 9.80003C9.50001 9.80003 9.80001 9.90003 10.1 10C10.4 10.1 10.7 10.3 10.9 10.4C11.1 10.5 11.3 10.8 11.4 11.1C11.5 11.4 11.6 11.6 11.6 11.9C11.6 12.3 11.5 12.6 11.3 12.9C11.1 13.2 10.9 13.5 10.6 13.7C10.9 13.9 11.2 14.1 11.4 14.3C11.6 14.5 11.8 14.7 11.9 15C12 15.3 12.1 15.5 12.1 15.8C12.1 16.2 12 16.5 11.9 16.8C11.8 17.1 11.5 17.4 11.3 17.7C11.1 18 10.7 18.2 10.3 18.3C9.9 18.4 9.5 18.5 9 18.5C8.5 18.5 8.1 18.4 7.7 18.2C7.3 18 7 17.8 6.8 17.6C6.6 17.4 6.4 17.1 6.3 16.8C6.2 16.5 6.10001 16.3 6.10001 16.1C6.10001 15.9 6.2 15.7 6.3 15.6C6.4 15.5 6.6 15.4 6.8 15.4C6.9 15.4 7.00001 15.4 7.10001 15.5C7.20001 15.6 7.3 15.6 7.3 15.7C7.5 16.2 7.7 16.6 8 16.9C8.3 17.2 8.6 17.3 9 17.3C9.2 17.3 9.5 17.2 9.7 17.1C9.9 17 10.1 16.8 10.3 16.6C10.5 16.4 10.5 16.1 10.5 15.8C10.5 15.3 10.4 15 10.1 14.7C9.80001 14.4 9.50001 14.3 9.10001 14.3C9.00001 14.3 8.9 14.3 8.7 14.3C8.5 14.3 8.39999 14.3 8.39999 14.3C8.19999 14.3 7.99999 14.2 7.89999 14.1C7.79999 14 7.7 13.8 7.7 13.7C7.7 13.5 7.79999 13.4 7.89999 13.2C7.99999 13 8.2 13 8.5 13H8.8V13.1ZM15.3 17.5V12.2C14.3 13 13.6 13.3 13.3 13.3C13.1 13.3 13 13.2 12.9 13.1C12.8 13 12.7 12.8 12.7 12.6C12.7 12.4 12.8 12.3 12.9 12.2C13 12.1 13.2 12 13.6 11.8C14.1 11.6 14.5 11.3 14.7 11.1C14.9 10.9 15.2 10.6 15.5 10.3C15.8 10 15.9 9.80003 15.9 9.70003C15.9 9.60003 16.1 9.60004 16.3 9.60004C16.5 9.60004 16.7 9.70003 16.8 9.80003C16.9 9.90003 17 10.2 17 10.5V17.2C17 18 16.7 18.4 16.2 18.4C16 18.4 15.8 18.3 15.6 18.2C15.4 18.1 15.3 17.8 15.3 17.5Z\" fill=\"currentColor\"/></svg></span>";

        //gen001.svg
        public const string House =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path d=\"M11 2.375L2 9.575V20.575C2 21.175 2.4 21.575 3 21.575H9C9.6 21.575 10 21.175 10 20.575V14.575C10 13.975 10.4 13.575 11 13.575H13C13.6 13.575 14 13.975 14 14.575V20.575C14 21.175 14.4 21.575 15 21.575H21C21.6 21.575 22 21.175 22 20.575V9.575L13 2.375C12.4 1.875 11.6 1.875 11 2.375Z\" fill=\"currentColor\"/></svg></span>";

        //gen029.svg
        public const string Start =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path d=\"M11.1359 4.48359C11.5216 3.82132 12.4784 3.82132 12.8641 4.48359L15.011 8.16962C15.1523 8.41222 15.3891 8.58425 15.6635 8.64367L19.8326 9.54646C20.5816 9.70867 20.8773 10.6186 20.3666 11.1901L17.5244 14.371C17.3374 14.5803 17.2469 14.8587 17.2752 15.138L17.7049 19.382C17.7821 20.1445 17.0081 20.7069 16.3067 20.3978L12.4032 18.6777C12.1463 18.5645 11.8537 18.5645 11.5968 18.6777L7.69326 20.3978C6.99192 20.7069 6.21789 20.1445 6.2951 19.382L6.7248 15.138C6.75308 14.8587 6.66264 14.5803 6.47558 14.371L3.63339 11.1901C3.12273 10.6186 3.41838 9.70867 4.16744 9.54646L8.3365 8.64367C8.61089 8.58425 8.84767 8.41222 8.98897 8.16962L11.1359 4.48359Z\" fill=\"currentColor\"/></svg></span>";

        //gen048.svg
        public const string EscudoCheck =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M20.5543 4.37824L12.1798 2.02473C12.0626 1.99176 11.9376 1.99176 11.8203 2.02473L3.44572 4.37824C3.18118 4.45258 3 4.6807 3 4.93945V13.569C3 14.6914 3.48509 15.8404 4.4417 16.984C5.17231 17.8575 6.18314 18.7345 7.446 19.5909C9.56752 21.0295 11.6566 21.912 11.7445 21.9488C11.8258 21.9829 11.9129 22 12.0001 22C12.0872 22 12.1744 21.983 12.2557 21.9488C12.3435 21.912 14.4326 21.0295 16.5541 19.5909C17.8169 18.7345 18.8277 17.8575 19.5584 16.984C20.515 15.8404 21 14.6914 21 13.569V4.93945C21 4.6807 20.8189 4.45258 20.5543 4.37824Z\" fill=\"currentColor\"/><path d=\"M10.5606 11.3042L9.57283 10.3018C9.28174 10.0065 8.80522 10.0065 8.51412 10.3018C8.22897 10.5912 8.22897 11.0559 8.51412 11.3452L10.4182 13.2773C10.8099 13.6747 11.451 13.6747 11.8427 13.2773L15.4859 9.58051C15.771 9.29117 15.771 8.82648 15.4859 8.53714C15.1948 8.24176 14.7183 8.24176 14.4272 8.53714L11.7002 11.3042C11.3869 11.6221 10.874 11.6221 10.5606 11.3042Z\" fill=\"currentColor\"/></svg></span>";

        //gen050.svg
        public const string EscudoUnCheck =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M20.5543 4.37824L12.1798 2.02473C12.0626 1.99176 11.9376 1.99176 11.8203 2.02473L3.44572 4.37824C3.18118 4.45258 3 4.6807 3 4.93945V13.569C3 14.6914 3.48509 15.8404 4.4417 16.984C5.17231 17.8575 6.18314 18.7345 7.446 19.5909C9.56752 21.0295 11.6566 21.912 11.7445 21.9488C11.8258 21.9829 11.9129 22 12.0001 22C12.0872 22 12.1744 21.983 12.2557 21.9488C12.3435 21.912 14.4326 21.0295 16.5541 19.5909C17.8169 18.7345 18.8277 17.8575 19.5584 16.984C20.515 15.8404 21 14.6914 21 13.569V4.93945C21 4.6807 20.8189 4.45258 20.5543 4.37824Z\" fill=\"currentColor\"/><rect x=\"9\" y=\"13.0283\" width=\"7.3536\" height=\"1.2256\" rx=\"0.6128\" transform=\"rotate(-45 9 13.0283)\" fill=\"currentColor\"/><rect x=\"9.86664\" y=\"7.93359\" width=\"7.3536\" height=\"1.2256\" rx=\"0.6128\" transform=\"rotate(45 9.86664 7.93359)\" fill=\"currentColor\"/></svg></span>";

        //gen013.svg
        public const string Reloj =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M20.9 12.9C20.3 12.9 19.9 12.5 19.9 11.9C19.9 11.3 20.3 10.9 20.9 10.9H21.8C21.3 6.2 17.6 2.4 12.9 2V2.9C12.9 3.5 12.5 3.9 11.9 3.9C11.3 3.9 10.9 3.5 10.9 2.9V2C6.19999 2.5 2.4 6.2 2 10.9H2.89999C3.49999 10.9 3.89999 11.3 3.89999 11.9C3.89999 12.5 3.49999 12.9 2.89999 12.9H2C2.5 17.6 6.19999 21.4 10.9 21.8V20.9C10.9 20.3 11.3 19.9 11.9 19.9C12.5 19.9 12.9 20.3 12.9 20.9V21.8C17.6 21.3 21.4 17.6 21.8 12.9H20.9Z\" fill=\"currentColor\"/><path d=\"M16.9 10.9H13.6C13.4 10.6 13.2 10.4 12.9 10.2V5.90002C12.9 5.30002 12.5 4.90002 11.9 4.90002C11.3 4.90002 10.9 5.30002 10.9 5.90002V10.2C10.6 10.4 10.4 10.6 10.2 10.9H9.89999C9.29999 10.9 8.89999 11.3 8.89999 11.9C8.89999 12.5 9.29999 12.9 9.89999 12.9H10.2C10.4 13.2 10.6 13.4 10.9 13.6V13.9C10.9 14.5 11.3 14.9 11.9 14.9C12.5 14.9 12.9 14.5 12.9 13.9V13.6C13.2 13.4 13.4 13.2 13.6 12.9H16.9C17.5 12.9 17.9 12.5 17.9 11.9C17.9 11.3 17.5 10.9 16.9 10.9Z\" fill=\"currentColor\"/></svg></span>";

        //gen026.svg
        public const string Default =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24px\" height=\"24px\" viewBox=\"0 0 24 24\"><path d=\"M10.0813 3.7242C10.8849 2.16438 13.1151 2.16438 13.9187 3.7242V3.7242C14.4016 4.66147 15.4909 5.1127 16.4951 4.79139V4.79139C18.1663 4.25668 19.7433 5.83365 19.2086 7.50485V7.50485C18.8873 8.50905 19.3385 9.59842 20.2758 10.0813V10.0813C21.8356 10.8849 21.8356 13.1151 20.2758 13.9187V13.9187C19.3385 14.4016 18.8873 15.491 19.2086 16.4951V16.4951C19.7433 18.1663 18.1663 19.7433 16.4951 19.2086V19.2086C15.491 18.8873 14.4016 19.3385 13.9187 20.2758V20.2758C13.1151 21.8356 10.8849 21.8356 10.0813 20.2758V20.2758C9.59842 19.3385 8.50905 18.8873 7.50485 19.2086V19.2086C5.83365 19.7433 4.25668 18.1663 4.79139 16.4951V16.4951C5.1127 15.491 4.66147 14.4016 3.7242 13.9187V13.9187C2.16438 13.1151 2.16438 10.8849 3.7242 10.0813V10.0813C4.66147 9.59842 5.1127 8.50905 4.79139 7.50485V7.50485C4.25668 5.83365 5.83365 4.25668 7.50485 4.79139V4.79139C8.50905 5.1127 9.59842 4.66147 10.0813 3.7242V3.7242Z\" fill=\"#00A3FF\"/>  <path class=\"permanent\" d=\"M14.8563 9.1903C15.0606 8.94984 15.3771 8.9385 15.6175 9.14289C15.858 9.34728 15.8229 9.66433 15.6185 9.9048L11.863 14.6558C11.6554 14.9001 11.2876 14.9258 11.048 14.7128L8.47656 12.4271C8.24068 12.2174 8.21944 11.8563 8.42911 11.6204C8.63877 11.3845 8.99996 11.3633 9.23583 11.5729L11.3706 13.4705L14.8563 9.1903Z\" fill=\"white\"/></svg></span>";
    }

    public static class Comunication
    {
        //Adjunto
        public const string Com008 =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M4.425 20.525C2.525 18.625 2.525 15.525 4.425 13.525L14.825 3.125C16.325 1.625 18.825 1.625 20.425 3.125C20.825 3.525 20.825 4.12502 20.425 4.52502C20.025 4.92502 19.425 4.92502 19.025 4.52502C18.225 3.72502 17.025 3.72502 16.225 4.52502L5.82499 14.925C4.62499 16.125 4.62499 17.925 5.82499 19.125C7.02499 20.325 8.82501 20.325 10.025 19.125L18.425 10.725C18.825 10.325 19.425 10.325 19.825 10.725C20.225 11.125 20.225 11.725 19.825 12.125L11.425 20.525C9.525 22.425 6.425 22.425 4.425 20.525Z\" fill=\"currentColor\"/><path d=\"M9.32499 15.625C8.12499 14.425 8.12499 12.625 9.32499 11.425L14.225 6.52498C14.625 6.12498 15.225 6.12498 15.625 6.52498C16.025 6.92498 16.025 7.525 15.625 7.925L10.725 12.8249C10.325 13.2249 10.325 13.8249 10.725 14.2249C11.125 14.6249 11.725 14.6249 12.125 14.2249L19.125 7.22493C19.525 6.82493 19.725 6.425 19.725 5.925C19.725 5.325 19.525 4.825 19.125 4.425C18.725 4.025 18.725 3.42498 19.125 3.02498C19.525 2.62498 20.125 2.62498 20.525 3.02498C21.325 3.82498 21.725 4.825 21.725 5.925C21.725 6.925 21.325 7.82498 20.525 8.52498L13.525 15.525C12.325 16.725 10.525 16.725 9.32499 15.625Z\" fill=\"currentColor\"/></svg></span>";

        //com010.svg
        public const string Email =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path d=\"M6 8.725C6 8.125 6.4 7.725 7 7.725H14L18 11.725V12.925L22 9.725L12.6 2.225C12.2 1.925 11.7 1.925 11.4 2.225L2 9.725L6 12.925V8.725Z\" fill=\"currentColor\"/><path opacity=\"0.3\" d=\"M22 9.72498V20.725C22 21.325 21.6 21.725 21 21.725H3C2.4 21.725 2 21.325 2 20.725V9.72498L11.4 17.225C11.8 17.525 12.3 17.525 12.6 17.225L22 9.72498ZM15 11.725H18L14 7.72498V10.725C14 11.325 14.4 11.725 15 11.725Z\" fill=\"currentColor\"/></svg></span>";

        //com013.svg
        public const string User =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path d=\"M6.28548 15.0861C7.34369 13.1814 9.35142 12 11.5304 12H12.4696C14.6486 12 16.6563 13.1814 17.7145 15.0861L19.3493 18.0287C20.0899 19.3618 19.1259 21 17.601 21H6.39903C4.87406 21 3.91012 19.3618 4.65071 18.0287L6.28548 15.0861Z\" fill=\"currentColor\"/><rect opacity=\"0.3\" x=\"8\" y=\"3\" width=\"8\" height=\"8\" rx=\"4\" fill=\"currentColor\"/></svg></span>";
    }

    public static class Coding
    {
        public const string VerMas =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M19.0687 17.9688H11.0687C10.4687 17.9688 10.0687 18.3687 10.0687 18.9688V19.9688C10.0687 20.5687 10.4687 20.9688 11.0687 20.9688H19.0687C19.6687 20.9688 20.0687 20.5687 20.0687 19.9688V18.9688C20.0687 18.3687 19.6687 17.9688 19.0687 17.9688Z\" fill=\"black\"/><path d=\"M4.06875 17.9688C3.86875 17.9688 3.66874 17.8688 3.46874 17.7688C2.96874 17.4688 2.86875 16.8688 3.16875 16.3688L6.76874 10.9688L3.16875 5.56876C2.86875 5.06876 2.96874 4.46873 3.46874 4.16873C3.96874 3.86873 4.56875 3.96878 4.86875 4.46878L8.86875 10.4688C9.06875 10.7688 9.06875 11.2688 8.86875 11.5688L4.86875 17.5688C4.66875 17.7688 4.36875 17.9688 4.06875 17.9688Z\" fill=\"black\"/></svg></span>";

        //cod001.svg
        public const string Config =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M22.1 11.5V12.6C22.1 13.2 21.7 13.6 21.2 13.7L19.9 13.9C19.7 14.7 19.4 15.5 18.9 16.2L19.7 17.2999C20 17.6999 20 18.3999 19.6 18.7999L18.8 19.6C18.4 20 17.8 20 17.3 19.7L16.2 18.9C15.5 19.3 14.7 19.7 13.9 19.9L13.7 21.2C13.6 21.7 13.1 22.1 12.6 22.1H11.5C10.9 22.1 10.5 21.7 10.4 21.2L10.2 19.9C9.4 19.7 8.6 19.4 7.9 18.9L6.8 19.7C6.4 20 5.7 20 5.3 19.6L4.5 18.7999C4.1 18.3999 4.1 17.7999 4.4 17.2999L5.2 16.2C4.8 15.5 4.4 14.7 4.2 13.9L2.9 13.7C2.4 13.6 2 13.1 2 12.6V11.5C2 10.9 2.4 10.5 2.9 10.4L4.2 10.2C4.4 9.39995 4.7 8.60002 5.2 7.90002L4.4 6.79993C4.1 6.39993 4.1 5.69993 4.5 5.29993L5.3 4.5C5.7 4.1 6.3 4.10002 6.8 4.40002L7.9 5.19995C8.6 4.79995 9.4 4.39995 10.2 4.19995L10.4 2.90002C10.5 2.40002 11 2 11.5 2H12.6C13.2 2 13.6 2.40002 13.7 2.90002L13.9 4.19995C14.7 4.39995 15.5 4.69995 16.2 5.19995L17.3 4.40002C17.7 4.10002 18.4 4.1 18.8 4.5L19.6 5.29993C20 5.69993 20 6.29993 19.7 6.79993L18.9 7.90002C19.3 8.60002 19.7 9.39995 19.9 10.2L21.2 10.4C21.7 10.5 22.1 11 22.1 11.5ZM12.1 8.59998C10.2 8.59998 8.6 10.2 8.6 12.1C8.6 14 10.2 15.6 12.1 15.6C14 15.6 15.6 14 15.6 12.1C15.6 10.2 14 8.59998 12.1 8.59998Z\" fill=\"currentColor\"/><path d=\"M17.1 12.1C17.1 14.9 14.9 17.1 12.1 17.1C9.30001 17.1 7.10001 14.9 7.10001 12.1C7.10001 9.29998 9.30001 7.09998 12.1 7.09998C14.9 7.09998 17.1 9.29998 17.1 12.1ZM12.1 10.1C11 10.1 10.1 11 10.1 12.1C10.1 13.2 11 14.1 12.1 14.1C13.2 14.1 14.1 13.2 14.1 12.1C14.1 11 13.2 10.1 12.1 10.1Z\" fill=\"currentColor\"/></svg></span>";
    }

    public static class Graphs
    {
        //gra002.svg
        public const string Area =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M20 8L12.5 5L5 14V19H20V8Z\" fill=\"currentColor\"/><path d=\"M21 18H6V3C6 2.4 5.6 2 5 2C4.4 2 4 2.4 4 3V18H3C2.4 18 2 18.4 2 19C2 19.6 2.4 20 3 20H4V21C4 21.6 4.4 22 5 22C5.6 22 6 21.6 6 21V20H21C21.6 20 22 19.6 22 19C22 18.4 21.6 18 21 18Z\" fill=\"currentColor\"/></svg></span>";
    }

    public static class Ecommerce
    {
        //ecm011.svg
        public const string Porcentaje =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M21.6 11.2L19.3 8.89998V5.59993C19.3 4.99993 18.9 4.59993 18.3 4.59993H14.9L12.6 2.3C12.2 1.9 11.6 1.9 11.2 2.3L8.9 4.59993H5.6C5 4.59993 4.6 4.99993 4.6 5.59993V8.89998L2.3 11.2C1.9 11.6 1.9 12.1999 2.3 12.5999L4.6 14.9V18.2C4.6 18.8 5 19.2 5.6 19.2H8.9L11.2 21.5C11.6 21.9 12.2 21.9 12.6 21.5L14.9 19.2H18.2C18.8 19.2 19.2 18.8 19.2 18.2V14.9L21.5 12.5999C22 12.1999 22 11.6 21.6 11.2Z\" fill=\"currentColor\"/><path d=\"M11.3 9.40002C11.3 10.2 11.1 10.9 10.7 11.3C10.3 11.7 9.8 11.9 9.2 11.9C8.8 11.9 8.40001 11.8 8.10001 11.6C7.80001 11.4 7.50001 11.2 7.40001 10.8C7.20001 10.4 7.10001 10 7.10001 9.40002C7.10001 8.80002 7.20001 8.4 7.30001 8C7.40001 7.6 7.7 7.29998 8 7.09998C8.3 6.89998 8.7 6.80005 9.2 6.80005C9.5 6.80005 9.80001 6.9 10.1 7C10.4 7.1 10.6 7.3 10.8 7.5C11 7.7 11.1 8.00005 11.2 8.30005C11.3 8.60005 11.3 9.00002 11.3 9.40002ZM10.1 9.40002C10.1 8.80002 10 8.39998 9.90001 8.09998C9.80001 7.79998 9.6 7.70007 9.2 7.70007C9 7.70007 8.8 7.80002 8.7 7.90002C8.6 8.00002 8.50001 8.2 8.40001 8.5C8.40001 8.7 8.30001 9.10002 8.30001 9.40002C8.30001 9.80002 8.30001 10.1 8.40001 10.4C8.40001 10.6 8.5 10.8 8.7 11C8.8 11.1 9 11.2001 9.2 11.2001C9.5 11.2001 9.70001 11.1 9.90001 10.8C10 10.4 10.1 10 10.1 9.40002ZM14.9 7.80005L9.40001 16.7001C9.30001 16.9001 9.10001 17.1 8.90001 17.1C8.80001 17.1 8.70001 17.1 8.60001 17C8.50001 16.9 8.40001 16.8001 8.40001 16.7001C8.40001 16.6001 8.4 16.5 8.5 16.4L14 7.5C14.1 7.3 14.2 7.19998 14.3 7.09998C14.4 6.99998 14.5 7 14.6 7C14.7 7 14.8 6.99998 14.9 7.09998C15 7.19998 15 7.30002 15 7.40002C15.2 7.30002 15.1 7.50005 14.9 7.80005ZM16.6 14.2001C16.6 15.0001 16.4 15.7 16 16.1C15.6 16.5 15.1 16.7001 14.5 16.7001C14.1 16.7001 13.7 16.6 13.4 16.4C13.1 16.2 12.8 16 12.7 15.6C12.5 15.2 12.4 14.8001 12.4 14.2001C12.4 13.3001 12.6 12.7 12.9 12.3C13.2 11.9 13.7 11.7001 14.5 11.7001C14.8 11.7001 15.1 11.8 15.4 11.9C15.7 12 15.9 12.2 16.1 12.4C16.3 12.6 16.4 12.9001 16.5 13.2001C16.6 13.4001 16.6 13.8001 16.6 14.2001ZM15.4 14.1C15.4 13.5 15.3 13.1 15.2 12.9C15.1 12.6 14.9 12.5 14.5 12.5C14.3 12.5 14.1 12.6001 14 12.7001C13.9 12.8001 13.8 13.0001 13.7 13.2001C13.6 13.4001 13.6 13.8 13.6 14.1C13.6 14.7 13.7 15.1 13.8 15.4C13.9 15.7 14.1 15.8 14.5 15.8C14.8 15.8 15 15.7 15.2 15.4C15.3 15.2 15.4 14.7 15.4 14.1Z\" fill=\"currentColor\"/></svg></span>";
    }

    public static class Electronic
    {
        //elc002.svg
        public const string MovilPhone =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path d=\"M6 21C6 21.6 6.4 22 7 22H17C17.6 22 18 21.6 18 21V20H6V21Z\" fill=\"currentColor\"/><path opacity=\"0.3\" d=\"M17 2H7C6.4 2 6 2.4 6 3V20H18V3C18 2.4 17.6 2 17 2Z\" fill=\"currentColor\"/><path d=\"M12 4C11.4 4 11 3.6 11 3V2H13V3C13 3.6 12.6 4 12 4Z\" fill=\"currentColor\"/></svg></span>";
    }

    public static class Abstract
    {
        //Menu
        public const string abs015 =
            "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\" {3}><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path d=\"M21 7H3C2.4 7 2 6.6 2 6V4C2 3.4 2.4 3 3 3H21C21.6 3 22 3.4 22 4V6C22 6.6 21.6 7 21 7Z\" fill=\"currentColor\"/><path opacity=\"0.3\" d=\"M21 14H3C2.4 14 2 13.6 2 13V11C2 10.4 2.4 10 3 10H21C21.6 10 22 10.4 22 11V13C22 13.6 21.6 14 21 14ZM22 20V18C22 17.4 21.6 17 21 17H3C2.4 17 2 17.4 2 18V20C2 20.6 2.4 21 3 21H21C21.6 21 22 20.6 22 20Z\" fill=\"currentColor\"/></svg></span>";
    }

    public static class Layout
    {
        //Vista
        public const string lay008 =
            "<span class=\"svg-icon svg-icon-muted svg-icon-2hx\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path d=\"M20 7H3C2.4 7 2 6.6 2 6V3C2 2.4 2.4 2 3 2H20C20.6 2 21 2.4 21 3V6C21 6.6 20.6 7 20 7ZM7 9H3C2.4 9 2 9.4 2 10V20C2 20.6 2.4 21 3 21H7C7.6 21 8 20.6 8 20V10C8 9.4 7.6 9 7 9Z\" fill=\"currentColor\"/><path opacity=\"0.3\" d=\"M20 21H11C10.4 21 10 20.6 10 20V10C10 9.4 10.4 9 11 9H20C20.6 9 21 9.4 21 10V20C21 20.6 20.6 21 20 21Z\" fill=\"currentColor\"/></svg></span>";
    }
}

public class IconModel
{
    public IconModel(string iconType)
    {
        IconType = iconType;
    }
    // -1 = CssLg {1} {2}
    // 0 = CssStandart {1} {2}
    // 1 = CssSmall
    // 2 = CssIcon
    // 3 = Value Button
    // 4 = Tooltip Button
    // 5 = Icono
    // 6 = Color icono

    private static string IconType { get; set; }

    public string OtherCss { get; set; }

    public string Title { get; set; }

    public int Color { get; set; } = EnuCommon.IconConfig.Color.Muted;
    public int Size { get; set; } = EnuCommon.IconConfig.Size._1;

    private string TxColor
    {
        get
        {
            return Color switch
            {
                EnuCommon.IconConfig.Color.White => "white",
                EnuCommon.IconConfig.Color.Primary => "primary",
                EnuCommon.IconConfig.Color.Secondary => "secondary",
                EnuCommon.IconConfig.Color.Light => "light",
                EnuCommon.IconConfig.Color.Success => "success",
                EnuCommon.IconConfig.Color.Info => "info",
                EnuCommon.IconConfig.Color.Warning => "warning",
                EnuCommon.IconConfig.Color.Danger => "danger",
                EnuCommon.IconConfig.Color.Dark => "dark",
                EnuCommon.IconConfig.Color.Muted => "muted",
                EnuCommon.IconConfig.Color.Gray100 => "gray-100",
                EnuCommon.IconConfig.Color.Gray200 => "gray-200",
                EnuCommon.IconConfig.Color.Gray300 => "gray-300",
                EnuCommon.IconConfig.Color.Gray400 => "gray-400",
                EnuCommon.IconConfig.Color.Gray500 => "gray-500",
                EnuCommon.IconConfig.Color.Gray600 => "gray-600",
                EnuCommon.IconConfig.Color.Gray700 => "gray-700",
                EnuCommon.IconConfig.Color.Gray800 => "gray-800",
                EnuCommon.IconConfig.Color.Gray900 => "gray-900",
                _ => throw new Exception("not Color ")
            };
        }
    }

    public string TxSize
    {
        get
        {
            return Size switch
            {
                EnuCommon.IconConfig.Size._1 => "1",
                EnuCommon.IconConfig.Size._2 => "2",
                EnuCommon.IconConfig.Size._3 => "3",
                EnuCommon.IconConfig.Size._4 => "4",
                EnuCommon.IconConfig.Size._5 => "5",
                EnuCommon.IconConfig.Size._6 => "6",
                EnuCommon.IconConfig.Size._7 => "7",
                EnuCommon.IconConfig.Size._8 => "8",
                EnuCommon.IconConfig.Size._9 => "9",
                EnuCommon.IconConfig.Size._10 => "10",
                EnuCommon.IconConfig.Size.Base => "base",
                EnuCommon.IconConfig.Size.Fluid => "fluid",
                EnuCommon.IconConfig.Size._2x => "2x",
                EnuCommon.IconConfig.Size._2qx => "2qx",
                EnuCommon.IconConfig.Size._2hx => "2hx",
                EnuCommon.IconConfig.Size._2tx => "2tx",
                EnuCommon.IconConfig.Size._3x => "3x",
                EnuCommon.IconConfig.Size._3qx => "qx",
                EnuCommon.IconConfig.Size._3hx => "3hx",
                EnuCommon.IconConfig.Size._3tx => "3tx",
                EnuCommon.IconConfig.Size._4x => "4x",
                EnuCommon.IconConfig.Size._4qx => "4qx",
                EnuCommon.IconConfig.Size._4hx => "4hx",
                EnuCommon.IconConfig.Size._4tx => "4tx",
                EnuCommon.IconConfig.Size._5x => "5x",
                EnuCommon.IconConfig.Size._5qx => "5qx",
                EnuCommon.IconConfig.Size._5hx => "5hx",
                EnuCommon.IconConfig.Size._5tx => "5tx",
                _ => throw new Exception("not size")
            };
        }
    }

    //private bool _leftPosition = false;
    //public ButtonModel(bool leftPosition)
    //{
    //    _leftPosition = leftPosition;
    //}


    //private static readonly Dictionary<int, string> IconList
    //                        = new()
    //                        {
    //                            //Color: svg-icon-{0}
    //                            //Size: svg-icon-{1}
    //                            //Others: {3}
    //                            {
    //                                EnuCommon.Icon.Type.General.Gen005,
    //                                "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M19 22H5C4.4 22 4 21.6 4 21V3C4 2.4 4.4 2 5 2H14L20 8V21C20 21.6 19.6 22 19 22ZM15 17C15 16.4 14.6 16 14 16H8C7.4 16 7 16.4 7 17C7 17.6 7.4 18 8 18H14C14.6 18 15 17.6 15 17ZM17 12C17 11.4 16.6 11 16 11H8C7.4 11 7 11.4 7 12C7 12.6 7.4 13 8 13H16C16.6 13 17 12.6 17 12ZM17 7C17 6.4 16.6 6 16 6H8C7.4 6 7 6.4 7 7C7 7.6 7.4 8 8 8H16C16.6 8 17 7.6 17 7Z\" fill=\"black\"/><path d=\"M15 8H20L14 2V7C14 7.6 14.4 8 15 8Z\" fill=\"black\"/></svg></span>"

    //                            },
    //                            {
    //                                EnuCommon.Icon.Type.General.Gen021,
    //                                "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><rect opacity=\"0.5\" x=\"17.0365\" y=\"15.1223\" width=\"8.15546\" height=\"2\" rx=\"1\" transform=\"rotate(45 17.0365 15.1223)\" fill=\"black\"/><path d=\"M11 19C6.55556 19 3 15.4444 3 11C3 6.55556 6.55556 3 11 3C15.4444 3 19 6.55556 19 11C19 15.4444 15.4444 19 11 19ZM11 5C7.53333 5 5 7.53333 5 11C5 14.4667 7.53333 17 11 17C14.4667 17 17 14.4667 17 11C17 7.53333 14.4667 5 11 5Z\" fill=\"black\"/></svg></span>"
    //                            },
    //                            {
    //                                EnuCommon.Icon.Type.General.Gen025,
    //                                "<span class=\"svg-icon svg-icon-{0} svg-icon-{1} {2}\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><rect x=\"2\" y=\"2\" width=\"9\" height=\"9\" rx=\"2\" fill=\"black\"/><rect opacity=\"0.3\" x=\"13\" y=\"2\" width=\"9\" height=\"9\" rx=\"2\" fill=\"black\"/><rect opacity=\"0.3\" x=\"13\" y=\"13\" width=\"9\" height=\"9\" rx=\"2\" fill=\"black\"/><rect opacity=\"0.3\" x=\"2\" y=\"13\" width=\"9\" height=\"9\" rx=\"2\" fill=\"black\"/></svg></span>"
    //                            },
    //                            {
    //                                EnuCommon.Icon.Type.Comunication.Com008,
    //                                "<span class=\"svg-icon svg-icon-muted svg-icon-2hx\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\"><path opacity=\"0.3\" d=\"M4.425 20.525C2.525 18.625 2.525 15.525 4.425 13.525L14.825 3.125C16.325 1.625 18.825 1.625 20.425 3.125C20.825 3.525 20.825 4.12502 20.425 4.52502C20.025 4.92502 19.425 4.92502 19.025 4.52502C18.225 3.72502 17.025 3.72502 16.225 4.52502L5.82499 14.925C4.62499 16.125 4.62499 17.925 5.82499 19.125C7.02499 20.325 8.82501 20.325 10.025 19.125L18.425 10.725C18.825 10.325 19.425 10.325 19.825 10.725C20.225 11.125 20.225 11.725 19.825 12.125L11.425 20.525C9.525 22.425 6.425 22.425 4.425 20.525Z\" fill=\"black\"/><path d=\"M9.32499 15.625C8.12499 14.425 8.12499 12.625 9.32499 11.425L14.225 6.52498C14.625 6.12498 15.225 6.12498 15.625 6.52498C16.025 6.92498 16.025 7.525 15.625 7.925L10.725 12.8249C10.325 13.2249 10.325 13.8249 10.725 14.2249C11.125 14.6249 11.725 14.6249 12.125 14.2249L19.125 7.22493C19.525 6.82493 19.725 6.425 19.725 5.925C19.725 5.325 19.525 4.825 19.125 4.425C18.725 4.025 18.725 3.42498 19.125 3.02498C19.525 2.62498 20.125 2.62498 20.525 3.02498C21.325 3.82498 21.725 4.825 21.725 5.925C21.725 6.925 21.325 7.82498 20.525 8.52498L13.525 15.525C12.325 16.725 10.525 16.725 9.32499 15.625Z\" fill=\"black\"/></svg></span>"
    //                            },

    //                        };

    public HtmlString Get()
    {
        var code = IconType;
        if (code.NoNulo())
        {
            if (Title.NoNulo()) Title = " title=\"" + Title + "\"";

            return new HtmlString(string.Format(code, TxColor, TxSize, OtherCss, Title));
        }

        return new HtmlString("");
    }

    //public Icon Icon
    //{
    //    get
    //    {
    //        return new Icon() { CoTipoBoton = CoTipoBoton };
    //    }
    //}
}

//public class IconModel2
//{

//    public HtmlString getSvgIcon(string path, string iconClass = "", string svgClass = "")
//    {
//        string full_path = path;
//        if (!File.Exists(path))
//        {
//            return new HtmlString("<!-- SVG file not found: " + path + " -->");
//        }

//        string svg_content = File.ReadAllText(path);

//        XmlDocument doc = new XmlDocument();
//        doc.LoadXml(svg_content);

//        //$dom = new DOMDocument();
//        //$dom->loadXML($svg_content);

//    // remove unwanted comments
//        $xpath = new DOMXPath($dom);
//        foreach ($xpath->query("//comment()") as $comment) {
//        $comment->parentNode->removeChild($comment);
//        }

//        // add class to svg
//        if (!empty($svgClass))
//        {
//            foreach ($dom->getElementsByTagName("svg") as $element) {
//            $element->setAttribute("class", $svgClass);
//            }
//        }

//    // remove unwanted tags
//    $title = $dom->getElementsByTagName("title");
//        if ($title["length"]) {
//        $dom->documentElement->removeChild($title[0]);
//        }

//    $desc = $dom->getElementsByTagName("desc");
//        if ($desc["length"]) {
//        $dom->documentElement->removeChild($desc[0]);
//        }

//    $defs = $dom->getElementsByTagName("defs");
//        if ($defs["length"]) {
//        $dom->documentElement->removeChild($defs[0]);
//        }

//    // remove unwanted id attribute in g tag
//    $g =  $dom->getElementsByTagName("g");
//        foreach ($g as $el) {
//        $el->removeAttribute("id");
//        }

//    $mask =  $dom->getElementsByTagName("mask");
//        foreach ($mask as $el) {
//        $el->removeAttribute("id");
//        }

//    $rect =  $dom->getElementsByTagName("rect");
//        foreach ($rect as $el) {
//        $el->removeAttribute("id");
//        }

//    $path =  $dom->getElementsByTagName("path");
//        foreach ($path as $el) {
//        $el->removeAttribute("id");
//        }

//    $circle =  $dom->getElementsByTagName("circle");
//        foreach ($circle as $el) {
//        $el->removeAttribute("id");
//        }

//    $use =  $dom->getElementsByTagName("use");
//        foreach ($use as $el) {
//        $el->removeAttribute("id");
//        }

//    $polygon =  $dom->getElementsByTagName("polygon");
//        foreach ($polygon as $el) {
//        $el->removeAttribute("id");
//        }

//    $ellipse =  $dom->getElementsByTagName("ellipse");
//        foreach ($ellipse as $el) {
//        $el->removeAttribute("id");
//        }

//    $string = $dom->saveXML($dom->documentElement);

//    // remove empty lines
//    $string = preg_replace("/(^[\r\n]*|[\r\n]+)[\s\t]*[\r\n]+/", "\n", $string);

//    $cls = array("svg-icon");

//        if (!empty($iconClass))
//        {
//        $cls = array_merge($cls, explode(" ", $iconClass));
//        }

//        return "<span class="" . implode(" ", $cls) . "">". $string. "</span>";
//    }
//}