#  Development Design Principle Definition  

HAMS is base on Microsoft Prism Library,MaterialDesign Controls is used for
user interaction,SQLite provide the data persistence support.

##  Design Principle
- The interaction relationship between the layer of MVVM pattern is shown as below  
  View => ViewModel => Model => Service.
- The communication message between modules is based on `String` type adopts Json
  string format,to avoid the impact of function expansion on infrastructure.  

##  Naming Principle  
- All of the framework components is started with `HAMS.Frame` prefix,which
  used for user interaction is followed by `Control` word.The extension
  components contain `Extension` word inject into framework.  
- The simple type only contain property definition is named with `Kind` suffix.  
- The enumeration type is named with `Part` suffix.  

##  MaterialDesign Controls Style Definition  
| Control Type  | Alias | Default Style | Whether to Specify Default Style |
| :-- | :-- | :-- | :-- |
| Window       | WIN | MaterialDesignWindow                          | True  |
| TextBlock    | TXT | MaterialDesignTextBlock                       | False |
| Button       | BTN | MaterialDesignOutlinedLightButton             | True  |
| ToggleButton | TGB | MaterialDesignSwitchToggleButton              | False |
| RadioButton  | RDB | MaterialDesignRadioButton                     | False |
| CheckBox     | CKB | MaterialDesignCheckBox                        | False |
| TextBox      | TXB | MaterialDesignTextBox                         | False |
| ComboBox     | CMB | MaterialDesignComboBox                        | False |
| ListBox      | LSB | MaterialDesignChoiceChipPrimaryOutlineListBox | True  |
| TreeView     | TRV | MaterialDesignTreeView                        | False |
