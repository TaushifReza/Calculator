# WPF Calculator Application

A desktop calculator application built with C# and WPF (.NET 8), featuring basic arithmetic operations, memory functions, and keyboard support.

## Project Structure
```
Calculator/
├── MainWindow.xaml          # UI layout and styling
├── MainWindow.xaml.cs       # Business logic and event handling
├── Calculator.csproj        # Project configuration
├── README.md               # This file
```

## Features

-  **Basic Operations**: Addition, Subtraction, Multiplication, Division
-  **Memory Functions**: M+, M-, MRC (Memory Recall), MC (Memory Clear)
-  **Decimal Support**: Full decimal number calculations
-  **Keyboard Shortcuts**: Complete keyboard navigation support
-  **Error Handling**: Division by zero protection and input validation
-  **Professional UI**: Modern design with visual feedback
-  **Display Formatting**: 20-character limit with proper number formatting

### Development Requirements
- **IDE**: Visual Studio 2022 (17.8 or later) or VS Code with C# extension
- **SDK**: .NET 8.0 SDK
- **Workload**: .NET desktop development workload (for Visual Studio)

## Installation & Setup

### Option 1: Building from Source

1. **Clone Repository**
   ```bash
   git clone [repository-url]
   cd Calculator
   ```

2. **Verify .NET 8 SDK Installation**
   ```bash
   dotnet --version
   # Should show 8.0.x or later
   ```

3. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

4. **Build Application**
   ```bash
   dotnet build --configuration Release
   ```

5. **Run Application**
   ```bash
   dotnet run
   ```

### Option 2: Visual Studio Setup

1. **Open Project**
   - Launch Visual Studio 2022
   - File → Open → Project/Solution
   - Select `Calculator.csproj`

2. **Build & Run**
   - Press `F5` to build and run with debugging
   - Or press `Ctrl+F5` to run without debugging

## Usage Guide

### Basic Operations

#### Number Input
- **Click number buttons** (0-9) or **use keyboard** to enter numbers
- **Decimal point**: Click `.` button or press `.` key
- **Double zero**: Click `00` button for quick entry

#### Arithmetic Operations
- **Addition**: Click `+` button or press `+` key
- **Subtraction**: Click `-` button or press `-` key  
- **Multiplication**: Click `×` button or press `*` key
- **Division**: Click `÷` button or press `/` key
- **Calculate**: Click `=` button or press `Enter` key

#### Clear Functions
- **AC (All Clear)**: Resets calculator completely - Click `AC` or press `Escape`
- **C (Clear Last)**: Removes last entered character - Click `C` or press `Backspace`

### Memory Functions

#### Memory Operations
- **M+ (Memory Add)**: Adds current display value to memory
- **M- (Memory Subtract)**: Subtracts current display value from memory
- **MRC (Memory Recall)**: Displays the value stored in memory
- **MC (Memory Clear)**: Clears the memory storage

#### Memory Indicator
- **Red "M"** appears in the top-left when memory contains a value
- **Disappears** when memory is cleared or contains zero

#### Memory Function Example
```
1. Enter: 100 × 2 → Press M+
   Display: Shows "2", Memory: Stores 200

2. Enter: 50 × 5 → Press M+  
   Display: Shows "5", Memory: Stores 450 (200+250)

3. Press MRC
   Display: Shows "450"
```

### Keyboard Shortcuts

#### Numbers & Operations
| Key | Function |
|-----|----------|
| `0-9` | Number input (both main and numpad) |
| `.` | Decimal point |
| `+` | Addition |
| `-` | Subtraction |
| `*` | Multiplication |
| `/` | Division |
| `Enter` | Calculate (equals) |

#### Control Functions
| Key | Function |
|-----|----------|
| `Escape` | All Clear (AC) |
| `Backspace` | Clear Last (C) |
| `Delete` | All Clear (AC) |

## Technical Specifications

### Keyboard Shortcuts

#### Numbers & Operations
| Key | Function |
|-----|----------|
| `0-9` | Number input (both main and numpad) |
| `.` | Decimal point |
| `+` | Addition |
| `-` | Subtraction |
| `*` | Multiplication |
| `/` | Division |
| `Enter` | Calculate (equals) |

#### Control Functions
| Key | Function |
|-----|----------|
| `Escape` | All Clear (AC) |
| `Backspace` | Clear Last (C) |
| `Delete` | All Clear (AC) |

## Technical Specifications

### Application Architecture
- **Framework**: .NET 8.0 WPF Application
- **Pattern**: MVVM-inspired code organization
- **UI Technology**: XAML with code-behind event handling
- **Data Types**: Double-precision floating point arithmetic

### Display Specifications
- **Maximum Length**: 20 characters
- **Alignment**: Right-aligned text
- **Font**: Consolas (monospace)
- **Background**: Black with white text
- **Default Value**: "0" (never empty)