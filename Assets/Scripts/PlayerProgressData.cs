using System;

namespace Pacman
{
    [Serializable]
    public class PlayerProgressData
    {
        public int TotalPellets;        
        public bool HasInvincibility;   
        public bool HasExtraLife;       
    }

    public class PlayerProgressModel
    {
        public int TotalPellets => m_Data.TotalPellets;
        public bool HasInvincibility => m_Data.HasInvincibility;
        public bool HasExtraLife => m_Data.HasExtraLife;

        public event Action<int> OnPelletsChanged;

        private PlayerProgressData m_Data;

        public PlayerProgressModel(PlayerProgressData data)
        {
            m_Data = data;
        }

        public void AddPellets(int amount)
        {
            m_Data.TotalPellets += amount;
            OnPelletsChanged?.Invoke(m_Data.TotalPellets);
        }

        public bool TrySpendPellets(int amount)
        {
            if (m_Data.TotalPellets < amount) return false;
            m_Data.TotalPellets -= amount;
            OnPelletsChanged?.Invoke(m_Data.TotalPellets);
            return true;
        }

        public void SetInvincibility(bool value) => m_Data.HasInvincibility = value;
        public void SetExtraLife(bool value) => m_Data.HasExtraLife = value;

        public PlayerProgressData GetData() => m_Data;
    }
}