using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using NotesAppApi.Model;

namespace NotesAppApi.Infrastructure
{
    public interface IRepository
    {
         Task<IEnumerable<Note>> GetAllNotes(); 
         Task<Note> GetNote(string id); 
         Task AddNote(Note item); 
         Task<DeleteResult> RemoveNote(string id); 
         Task<UpdateResult> UpdateNote(string id, string body); 
         Task<ReplaceOneResult> UpdateNoteDocument(string id, string body); 
         Task<DeleteResult> RemoveAllNotes(); 
         

         
    }
}