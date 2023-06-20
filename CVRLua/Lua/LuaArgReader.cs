using System;
using System.Collections.Generic;

namespace CVRLua.Lua
{
    class LuaArgReader
    {
        readonly LuaVM m_vm = null;
        int m_currentArgument = 1;
        readonly int m_argumentsCount;
        int m_returnCount = 0;
        bool m_hasErrors = false;
        string m_errorInfo;

        public LuaArgReader(IntPtr p_state)
        {
            m_vm = LuaVM.GetVM(p_state);

            if(m_vm != null)
                m_argumentsCount = m_vm.GetTop();
        }

        public void SetError(string p_error)
        {
            m_hasErrors = true;
            m_errorInfo = p_error;
        }
        public bool HasErrors() => m_hasErrors;
        public void LogError()
        {
            if((m_vm != null) && m_hasErrors)
            {
                m_vm.GetStackInfo(out string p_chunk, out int l_line);
                LuaLogger.Log("Warning: [\"{0}\"]:{1}: {2} at argument {3}", p_chunk, l_line, m_errorInfo, m_currentArgument);
            }
        }

        public int GetArgumentsCount() => m_argumentsCount;
        public int GetReturnValue() => m_returnCount;

        public void ReadBoolean(ref bool p_val)
        {
            if((m_vm != null) && !m_hasErrors)
            {
                if(m_currentArgument <= m_argumentsCount)
                {
                    if(m_vm.IsBoolean(m_currentArgument))
                    {
                        p_val = m_vm.ToBoolean(m_currentArgument);
                        m_currentArgument++;
                    }
                    else
                        SetError("Expected boolean");
                }
                else
                    SetError("Not enough arguments");
            }
        }

        public void ReadNextBoolean(ref bool p_val)
        {
            if((m_vm != null) && !m_hasErrors && (m_currentArgument <= m_argumentsCount))
            {
                if(m_vm.IsBoolean(m_currentArgument))
                    p_val = m_vm.ToBoolean(m_currentArgument);
                m_currentArgument++;
            }
        }

        public void ReadNumber(ref double p_val)
        {
            if((m_vm != null) && !m_hasErrors)
            {
                if(m_currentArgument <= m_argumentsCount)
                {
                    if(m_vm.IsNumber(m_currentArgument))
                    {
                        p_val = m_vm.ToNumber(m_currentArgument);
                        m_currentArgument++;
                    }
                    else
                        SetError("Expected number");
                }
                else
                    SetError("Not enough arguments");
            }
        }

        public void ReadNumber(ref float p_val)
        {
            if((m_vm != null) && !m_hasErrors)
            {
                if(m_currentArgument <= m_argumentsCount)
                {
                    if(m_vm.IsNumber(m_currentArgument))
                    {
                        p_val = (float)m_vm.ToNumber(m_currentArgument);
                        m_currentArgument++;
                    }
                    else
                        SetError("Expected number");
                }
                else
                    SetError("Not enough arguments");
            }
        }

        public void ReadNextNumber(ref double p_val)
        {
            if((m_vm != null) && !m_hasErrors && (m_currentArgument <= m_argumentsCount))
            {
                if(m_vm.IsNumber(m_currentArgument))
                    p_val = m_vm.ToNumber(m_currentArgument);
                m_currentArgument++;
            }
        }

        public void ReadNextNumber(ref float p_val)
        {
            if((m_vm != null) && !m_hasErrors && (m_currentArgument <= m_argumentsCount))
            {
                if(m_vm.IsNumber(m_currentArgument))
                    p_val = (float)m_vm.ToNumber(m_currentArgument);
                m_currentArgument++;
            }
        }

        public void ReadInteger(ref long p_val)
        {
            if((m_vm != null) && !m_hasErrors)
            {
                if(m_currentArgument <= m_argumentsCount)
                {
                    if(m_vm.IsInteger(m_currentArgument))
                    {
                        p_val = m_vm.ToInteger(m_currentArgument);
                        m_currentArgument++;
                    }
                    else
                        SetError("Expected integer");
                }
                else
                    SetError("Not enough arguments");
            }
        }

        public void ReadInteger(ref int p_val)
        {
            if((m_vm != null) && !m_hasErrors)
            {
                if(m_currentArgument <= m_argumentsCount)
                {
                    if(m_vm.IsInteger(m_currentArgument))
                    {
                        p_val = (int)m_vm.ToInteger(m_currentArgument);
                        m_currentArgument++;
                    }
                    else
                        SetError("Expected integer");
                }
                else
                    SetError("Not enough arguments");
            }
        }

        public void ReadNextInteger(ref long p_val)
        {
            if((m_vm != null) && !m_hasErrors && (m_currentArgument <= m_argumentsCount))
            {
                if(m_vm.IsInteger(m_currentArgument))
                    p_val = m_vm.ToInteger(m_currentArgument);
                m_currentArgument++;
            }
        }

        public void ReadNextInteger(ref int p_val)
        {
            if((m_vm != null) && !m_hasErrors && (m_currentArgument <= m_argumentsCount))
            {
                if(m_vm.IsInteger(m_currentArgument))
                    p_val = (int)m_vm.ToInteger(m_currentArgument);
                m_currentArgument++;
            }
        }

        public void ReadString(ref string p_val)
        {
            if((m_vm != null) && !m_hasErrors)
            {
                if(m_currentArgument <= m_argumentsCount)
                {
                    if(m_vm.IsString(m_currentArgument))
                    {
                        p_val = m_vm.ToString(m_currentArgument);
                        m_currentArgument++;
                    }
                    else
                        SetError("Expected string");
                }
                else
                    SetError("Not enough arguments");
            }
        }

        public void ReadNextString(ref string p_val)
        {
            if((m_vm != null) && !m_hasErrors && (m_currentArgument <= m_argumentsCount))
            {
                if(m_vm.IsString(m_currentArgument))
                    p_val = m_vm.ToString(m_currentArgument);
                m_currentArgument++;
            }
        }

        public void ReadObject<T>(ref T p_val) where T : class
        {
            if((m_vm != null) && !m_hasErrors)
            {
                if(m_currentArgument <= m_argumentsCount)
                {
                    if(m_vm.GetObject(ref p_val, m_currentArgument))
                        m_currentArgument++;
                    else
                        SetError("Invalid object or expected " + typeof(T).Name);
                }
                else
                    SetError("Not enough arguments");
            }
        }

        public void ReadNextObject<T>(ref T p_val) where T : class
        {
            if((m_vm != null) && !m_hasErrors && (m_currentArgument <= m_argumentsCount))
            {
                if(m_vm.IsObject(m_currentArgument))
                    m_vm.GetObject(ref p_val, m_currentArgument);
                m_currentArgument++;
            }
        }

        public void ReadEnum<T>(ref T p_enum) where T : struct
        {
            if((m_vm != null) && !m_hasErrors)
            {
                if(m_currentArgument <= m_argumentsCount)
                {
                    if(m_vm.IsString(m_currentArgument))
                    {
                        if(Enum.TryParse(m_vm.ToString(m_currentArgument), out p_enum))
                            m_currentArgument++;
                        else
                            SetError("Invalid enum");
                    }
                    else
                        SetError("Expected string");
                }
                else
                    SetError("Not enough arguments");
            }
        }

        public void ReadNextEnum<T>(ref T p_enum) where T : struct
        {
            if((m_vm != null) && !m_hasErrors && (m_currentArgument <= m_argumentsCount))
            {
                if(m_vm.IsString(m_currentArgument))
                    Enum.TryParse(m_vm.ToString(m_currentArgument), out p_enum);
                m_currentArgument++;
            }
        }

        public void ReadArguments(List<object> p_args)
        {
            if((m_vm != null) && !m_hasErrors)
            {
                while(m_currentArgument <= m_argumentsCount)
                {
                    p_args.Add(m_vm.ReadValue(m_currentArgument));
                    m_currentArgument++;
                }
            }
        }

        public void PushBoolean(bool p_val)
        {
            if(m_vm != null)
            {
                m_vm.PushBoolean(p_val);
                m_returnCount++;
            }
        }

        public void PushNumber(double p_val)
        {
            if(m_vm != null)
            {
                m_vm.PushNumber(p_val);
                m_returnCount++;
            }
        }

        public void PushInteger(long p_val)
        {
            if(m_vm != null)
            {
                m_vm.PushInteger(p_val);
                m_returnCount++;
            }
        }

        public void PushString(string p_str)
        {
            if(m_vm != null)
            {
                m_vm.PushString(p_str);
                m_returnCount++;
            }
        }

        public void PushObject(object p_obj)
        {
            if(m_vm != null)
            {
                m_vm.PushObject(p_obj);
                m_returnCount++;
            }
        }

        public void PushNil()
        {
            if(m_vm != null)
            {
                m_vm.PushNil();
                m_returnCount++;
            }
        }

        public void PushFunction(LuaInterop.lua_CFunction p_func)
        {
            if(m_vm != null)
            {
                m_vm.PushFunction(p_func);
                m_returnCount++;
            }
        }

        public void PushTable<T>(T[] p_array)
        {
            if(m_vm != null)
            {
                m_vm.PushTable(p_array);
                m_returnCount++;
            }
        }

        public void PushTable<T>(List<T> p_list)
        {
            if(m_vm != null)
            {
                m_vm.PushTable(p_list);
                m_returnCount++;
            }
        }

        public void PushTable(Dictionary<string, object> p_dict)
        {
            if(m_vm != null)
            {
                m_vm.PushTable(p_dict);
                m_returnCount++;
            }
        }

        public bool IsNextBoolean() => ((m_vm != null) && (m_currentArgument <= m_argumentsCount) && m_vm.IsBoolean(m_currentArgument));
        public bool IsNextNumber() => ((m_vm != null) && (m_currentArgument <= m_argumentsCount) && m_vm.IsNumber(m_currentArgument));
        public bool IsNextInteger() => ((m_vm != null) && (m_currentArgument <= m_argumentsCount) && m_vm.IsInteger(m_currentArgument));
        public bool IsNextString() => ((m_vm != null) && (m_currentArgument <= m_argumentsCount) && m_vm.IsString(m_currentArgument));
        public bool IsNextObject() => ((m_vm != null) && (m_currentArgument <= m_argumentsCount) && m_vm.IsObject(m_currentArgument));
        public bool IsNextNil() => ((m_vm != null) && (m_currentArgument <= m_argumentsCount) && m_vm.IsNil(m_currentArgument));

        public void Skip(int p_count = 1)
        {
            if((m_vm != null) && !m_hasErrors)
                m_currentArgument += p_count;
        }
    }
}
