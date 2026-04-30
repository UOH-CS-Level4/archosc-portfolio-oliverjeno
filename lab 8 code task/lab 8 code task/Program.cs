using System.Collections.Generic;
class TLBEntry
{
    public int VPN;
    public int PPN;
}
class TLB
{
    private List<TLBEntry> entries = new List<TLBEntry>(); private const int capacity = 4; // TLB capacity 
    public int? Lookup(int vpn)
    {
        foreach (var entry in entries)
        {
            if
    (entry.VPN == vpn) return entry.PPN;
        }
        return null; // TLB miss 
    }
    public void Insert(int vpn, int ppn)
    {
        if (entries.Count >= capacity)
            entries.RemoveAt(0); entries.Add(new TLBEntry { VPN = vpn, PPN = ppn });
    }
}
//1.Simulate TLB hits and misses. 
//2. Track the hit ratio after a sequence of address translations

class Program
{
    static void Main()
    {
        TLB tlb = new TLB(); 
        int[] virtualAddresses = { 0x0000, 0x1000, 0x2000, 0x3000, 0x0000, 0x1000, 0x4000 }; // Sample virtual addresses
        int hits = 0; 
        int total = virtualAddresses.Length; 
        foreach (var va in virtualAddresses)
        {
            int vpn = va >> 12; 
            int? ppn = tlb.Lookup(vpn);
            if (ppn.HasValue)
            {
                hits++;
                Console.WriteLine($"TLB Hit: VPN {vpn} and PPN {ppn.Value}");
            }
            else
            {
                Console.WriteLine($"TLB Miss: VPN {vpn}");
                tlb.Insert(vpn, vpn); 
            }
        }
        double hitRatio = (double)hits / total;
        Console.WriteLine($"Hit Ratio: {hitRatio:P2}");
    }
}